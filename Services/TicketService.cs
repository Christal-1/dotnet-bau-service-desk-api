using BaUServiceDeskApi.Contracts;
using BaUServiceDeskApi.Data;
using BaUServiceDeskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BaUServiceDeskApi.Services;

public class TicketService(TicketDbContext db) : ITicketService
{
    public async Task<IReadOnlyList<SupportTicket>> GetAllAsync(string? status, CancellationToken ct)
    {
        var query = db.Tickets.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Status == status);
        return await query.OrderByDescending(t => t.CreatedAtUtc).ToListAsync(ct);
    }

    public Task<SupportTicket?> GetAsync(int id, CancellationToken ct) =>
        db.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<SupportTicket> CreateAsync(CreateTicketRequest request, CancellationToken ct)
    {
        var ticket = new SupportTicket
        {
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Priority = request.Priority,
            RequestedBy = request.RequestedBy.Trim()
        };
        db.Tickets.Add(ticket);
        await db.SaveChangesAsync(ct);
        return ticket;
    }

    public async Task<bool> UpdateStatusAsync(int id, string status, CancellationToken ct)
    {
        var ticket = await db.Tickets.FindAsync([id], ct);
        if (ticket is null) return false;

        if (ticket.Status is "Resolved" or "Closed" && status == "InProgress")
            throw new InvalidOperationException("A resolved or closed ticket cannot move directly to InProgress.");

        ticket.Status = status;
        ticket.ResolvedAtUtc = status == "Resolved" ? DateTime.UtcNow : null;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var ticket = await db.Tickets.FindAsync([id], ct);
        if (ticket is null) return false;
        db.Tickets.Remove(ticket);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
