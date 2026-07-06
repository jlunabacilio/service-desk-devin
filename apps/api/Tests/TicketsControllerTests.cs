using api.Controllers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace api.Tests;

public class TicketsControllerTests
{
    [Fact]
    public void UpdateStatus_WithValidStatus_UpdatesTicket()
    {
        var store = new TicketStore();
        var controller = new TicketsController(store);
        var existing = store.GetAll().First();
        var originalUpdatedAt = existing.UpdatedAt;
        var dto = new UpdateTicketStatusDto { Status = TicketStatus.InProgress };

        var result = controller.UpdateStatus(existing.Id, dto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var ticket = Assert.IsType<Ticket>(okResult.Value);
        Assert.Equal(TicketStatus.InProgress, ticket.Status);
        Assert.True(ticket.UpdatedAt > originalUpdatedAt);
    }

    [Fact]
    public void UpdateStatus_WithNonExistentTicket_Returns404()
    {
        var store = new TicketStore();
        var controller = new TicketsController(store);
        var dto = new UpdateTicketStatusDto { Status = TicketStatus.Closed };

        var result = controller.UpdateStatus("does-not-exist", dto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void UpdateStatus_WithInvalidStatusValue_Returns400()
    {
        var store = new TicketStore();
        var controller = new TicketsController(store);
        var existing = store.GetAll().First();
        var dto = new UpdateTicketStatusDto { Status = (TicketStatus)99 };

        var result = controller.UpdateStatus(existing.Id, dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void UpdateStatus_WithInvalidModelState_Returns400()
    {
        var store = new TicketStore();
        var controller = new TicketsController(store);
        var existing = store.GetAll().First();
        controller.ModelState.AddModelError("Status", "Status is required");
        var dto = new UpdateTicketStatusDto();

        var result = controller.UpdateStatus(existing.Id, dto);

        var badRequestResult = Assert.IsType<ObjectResult>(result);
        Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
    }
}
