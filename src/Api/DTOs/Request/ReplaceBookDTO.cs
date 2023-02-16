using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class ReplaceBookDTO
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Author { get; init; }
    public int? Edition { get; init; }
    [Required]
    public string Language { get; init; }
    [Required]
    public string Publisher { get; init; }
    public int? Pages { get; init; }
    public int? Quantity { get; init; }
    [Required]
    public double? Price { get; init; }
    public int? Year { get; init; }
    [Required]
    public BookPreservation? Preservation { get; init; }
}
