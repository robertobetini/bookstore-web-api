using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateBookDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    public int? Edition { get; set; }
    [Required]
    public string Language { get; set; }
    [Required]
    public string Publisher { get; set; }
    public int? Pages { get; set; }
    public int? Quantity { get; set; }
    [Required]
    public double? Price { get; set; }
    public int? Year { get; set; }
    [Required]
    public BookPreservation? Preservation { get; set; }
}
