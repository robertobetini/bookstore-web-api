using Core.Entities.Enums;

namespace Api.DTOs.Request;

public class UpdateBookDTO
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public int? Edition { get; init; }
    public string? Language { get; init; }
    public string? Publisher { get; init; }
    public int? Pages { get; init; }
    public int? Quantity { get; init; }
    public double? Price { get; init; }
    public int? Year { get; init; }
    public BookPreservation? Preservation { get; init; }
}
