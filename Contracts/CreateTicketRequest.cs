using System.ComponentModel.DataAnnotations;

namespace BaUServiceDeskApi.Contracts;

public class CreateTicketRequest
{
    [Required, StringLength(120, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(1000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;
    [Required, RegularExpression("^(Low|Normal|High|Critical)$")]
    public string Priority { get; set; } = "Normal";
    [Required, StringLength(100, MinimumLength = 2)]
    public string RequestedBy { get; set; } = string.Empty;
}
