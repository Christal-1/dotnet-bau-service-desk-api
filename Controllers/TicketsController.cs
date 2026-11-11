using BaUServiceDeskApi.Contracts;
using BaUServiceDeskApi.Models;
using BaUServiceDeskApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaUServiceDeskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController(ITicketService service, ILogger<TicketsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SupportTicket>>> GetAll(
        [FromQuery] string? status, CancellationToken ct) =>
        Ok(await service.GetAllAsync(status, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupportTicket>> Get(int id, CancellationToken ct)
    {
        var ticket = await service.GetAsync(id, ct);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<SupportTicket>> Create(CreateTicketRequest request, CancellationToken ct)
    {
        var ticket = await service.CreateAsync(request, ct);
        logger.LogInformation("Created support ticket {TicketId}", ticket.Id);
        return CreatedAtAction(nameof(Get), new { id = ticket.Id }, ticket);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateTicketRequest request, CancellationToken ct)
    {
        try
        {
            return await service.UpdateStatusAsync(id, request.Status, ct)
                ? NoContent()
                : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) =>
        await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
