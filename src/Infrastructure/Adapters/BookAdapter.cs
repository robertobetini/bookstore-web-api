using Core.Entities;
using Infrastructure.DbEntities;

namespace Infrastructure.Adapters;

static class BookAdapter
{
    public static Book? ToDomainEntity(BookMongoDB? bookMongoDB)
    {
        if (bookMongoDB is null)
        {
            return default;
        }

        return new Book(
            bookMongoDB.Title,
            bookMongoDB.Author,
            bookMongoDB.Edition,
            bookMongoDB.Language,
            bookMongoDB.Publisher,
            bookMongoDB.Pages,
            bookMongoDB.Quantity,
            bookMongoDB.Price,
            bookMongoDB.Year,
            bookMongoDB.Preservation,
            bookMongoDB.IsDeleted);
    }

    public static BookMongoDB? ToMongoDBEntity(Book book, string? bookId = null)
    {
        if (book is null)
        {
            return default;
        }

        return new BookMongoDB(
            book.Title,
            book.Author,
            book.Edition,
            book.Language,
            book.Publisher,
            book.Pages,
            book.Quantity,
            book.Price,
            book.Year,
            book.Preservation,
            bookId);
    }

    public static IEnumerable<Book?> ToDomainEntities(IEnumerable<BookMongoDB> booksMongoDB)
    {
        return booksMongoDB.Select(bookMongoDB => ToDomainEntity(bookMongoDB));
    }

    public static IEnumerable<BookMongoDB?> ToMongoDBEntities(IEnumerable<Book> books)
    {
        return books.Select(book => ToMongoDBEntity(book));
    }
}
