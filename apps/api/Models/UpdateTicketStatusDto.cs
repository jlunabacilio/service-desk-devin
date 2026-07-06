using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class UpdateTicketStatusDto
{
    [Required]
    public TicketStatus Status { get; init; }
}
