using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("tickets")]
public class TicketsController(TicketStore store) : ControllerBase
{
    // GET /tickets
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<Ticket>>(StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(store.GetAll());

    // GET /tickets/{id}
    [HttpGet("{id}")]
    [ProducesResponseType<Ticket>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(string id)
    {
        var ticket = store.GetById(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    // POST /tickets
    [HttpPost]
    [ProducesResponseType<Ticket>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateTicketDto dto)
    {
        // [ApiController] validates DataAnnotations and returns 400 automatically,
        // but we check ModelState explicitly for clarity.
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var now = DateTimeOffset.UtcNow;
        var ticket = new Ticket
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Description = dto.Description,
            Status = TicketStatus.Open,
            Priority = dto.Priority,
            CreatedAt = now,
            UpdatedAt = now,
            AssigneeId = null,
            ReporterId = dto.ReporterId
        };

        store.Add(ticket);

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    // PATCH /tickets/{id}/status
    [HttpPatch("{id}/status")]
    [ProducesResponseType<Ticket>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateStatus(string id, [FromBody] UpdateTicketStatusDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        if (!Enum.IsDefined(typeof(TicketStatus), dto.Status))
            return BadRequest(new { error = $"Invalid ticket status: {dto.Status}" });

        var ticket = store.UpdateStatus(id, dto.Status);
        return ticket is null ? NotFound() : Ok(ticket);
    }
}
