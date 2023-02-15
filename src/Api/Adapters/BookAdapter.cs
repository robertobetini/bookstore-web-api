using Api.DTOs.Request;
using Api.DTOs.Response;
using Core.Entities;

namespace Api.Adapters;

static class BookAdapter
{
    public static Book ToDomainEntity(CreateBookDTO bookDTO)
    {
        if (bookDTO is null)
        {
            return default;
        }

        return new Book(
            bookDTO.Title,
            bookDTO.Author,
            bookDTO.Edition,
            bookDTO.Language,
            bookDTO.Publisher,
            bookDTO.Pages,
            bookDTO.Quantity,
            bookDTO.Price,
            bookDTO.Year,
            bookDTO.Preservation);
    }

    public static Book ToDomainEntity(UpdateBookDTO bookDTO)
    {
        if (bookDTO is null)
        {
            return default;
        }

        return new Book(
            bookDTO.Title,
            bookDTO.Author,
            bookDTO.Edition,
            bookDTO.Language,
            bookDTO.Publisher,
            bookDTO.Pages,
            bookDTO.Quantity,
            bookDTO.Price,
            bookDTO.Year,
            bookDTO.Preservation);
    }

    public static GetBookDTO ToGetBookDTO(Book book)
    {
        if (book is null)
        {
            return default;
        }

        return new GetBookDTO
        {
            Title = book!.Title,
            Author = book!.Author,
            Edition = book!.Edition,
            Language = book!.Language,
            Publisher = book!.Publisher,
            Pages = (int)book!.Pages,
            Quantity = (int)book!.Quantity,
            Price = (double)book!.Price,
            Year = (int)book!.Year,
            Preservation = book!.Preservation
        };
    }

    public static IEnumerable<GetBookDTO> ToGetBookDTOs(IEnumerable<Book> books)
    {
        return books.Select(book => ToGetBookDTO(book));
    }
}
