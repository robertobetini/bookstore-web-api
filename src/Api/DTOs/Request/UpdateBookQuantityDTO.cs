using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class UpdateBookQuantityDTO
{
    [Required]
    public int? Quantity { get; init; }
}
