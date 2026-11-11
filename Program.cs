using BaUServiceDeskApi.Data;
using BaUServiceDeskApi.Models;
using BaUServiceDeskApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Tickets")));
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
    db.Database.EnsureCreated();
    if (!db.Tickets.Any())
    {
        db.Tickets.AddRange(
            new SupportTicket { Title = "Policy search page returns an error", Description = "The internal policy search page returns HTTP 500 for valid policy numbers.", Priority = "High", Status = "Open", RequestedBy = "Operations Team", CreatedAtUtc = DateTime.UtcNow.AddHours(-4) },
            new SupportTicket { Title = "Update customer contact details", Description = "A BAU request to add an additional contact number to the customer profile.", Priority = "Normal", Status = "InProgress", RequestedBy = "Client Services", CreatedAtUtc = DateTime.UtcNow.AddDays(-1) },
            new SupportTicket { Title = "Monthly report export is slow", Description = "The monthly report export takes longer than expected for large date ranges.", Priority = "Critical", Status = "Open", RequestedBy = "Finance Team", CreatedAtUtc = DateTime.UtcNow.AddDays(-2) },
            new SupportTicket { Title = "Password reset assistance", Description = "User required assistance to reset their application password.", Priority = "Low", Status = "Resolved", RequestedBy = "Claims Support", CreatedAtUtc = DateTime.UtcNow.AddDays(-3), ResolvedAtUtc = DateTime.UtcNow.AddDays(-2) },
            new SupportTicket { Title = "Add policy status filter", Description = "Small enhancement request to filter policy records by their current status.", Priority = "Normal", Status = "Closed", RequestedBy = "Underwriting", CreatedAtUtc = DateTime.UtcNow.AddDays(-6) }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", application = "BAU Service Desk" }));
app.Run();

public partial class Program { }
