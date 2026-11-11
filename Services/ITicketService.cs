using BaUServiceDeskApi.Contracts;
using BaUServiceDeskApi.Models;

namespace BaUServiceDeskApi.Services;

public interface ITicketService
{
    Task<IReadOnlyList<SupportTicket>> GetAllAsync(string? status, CancellationToken ct);
    Task<SupportTicket?> GetAsync(int id, CancellationToken ct);
    Task<SupportTicket> CreateAsync(CreateTicketRequest request, CancellationToken ct);
    Task<bool> UpdateStatusAsync(int id, string status, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
