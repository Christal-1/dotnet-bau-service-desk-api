using System.ComponentModel.DataAnnotations;

namespace BaUServiceDeskApi.Contracts;

public class UpdateTicketRequest
{
    [Required, RegularExpression("^(Open|InProgress|Resolved|Closed)$")]
    public string Status { get; set; } = "Open";
}
