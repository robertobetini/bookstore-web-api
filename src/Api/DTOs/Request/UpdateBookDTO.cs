using Core.Entities.Enums;

namespace Api.DTOs.Request;

public class UpdateBookDTO
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int? Edition { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }
    public int? Pages { get; set; }
    public int? Quantity { get; set; }
    public double? Price { get; set; }
    public int? Year { get; set; }
    public BookPreservation? Preservation { get; set; }
}
