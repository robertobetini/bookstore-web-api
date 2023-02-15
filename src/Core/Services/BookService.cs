using Core.Entities;
using Core.Exceptions;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetManyAsync(CancellationToken cancellationToken = default)
    {
        return await _bookRepository.GetManyAsync(cancellationToken);
    }

    public async Task<Book> GetOneAsync(string bookId, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetOneAsync(bookId, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException();
        }

        return book;
    }

    public async Task<string> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        return await _bookRepository.Create(book, cancellationToken);
    }

    public async Task UpdateAsync(string bookId, Book book, CancellationToken cancellationToken = default)
    {
        await _bookRepository.UpdateAsync(bookId, book, cancellationToken);
    }

    public async Task DeleteAsync(string bookId, CancellationToken cancellationToken = default)
    {
        await _bookRepository.DeleteAsync(bookId, cancellationToken);
    }
}
