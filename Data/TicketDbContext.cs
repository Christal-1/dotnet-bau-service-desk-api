using BaUServiceDeskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BaUServiceDeskApi.Data;

public class TicketDbContext(DbContextOptions<TicketDbContext> options) : DbContext(options)
{
    public DbSet<SupportTicket> Tickets => Set<SupportTicket>();
}
