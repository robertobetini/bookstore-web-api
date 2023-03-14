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

    public async Task<IEnumerable<Book>> GetManyAsync(
        bool retriveDeleted = false, 
        CancellationToken cancellationToken = default)
    {
        return await _bookRepository.GetManyAsync(retriveDeleted, cancellationToken);
    }

    public async Task<Book> GetOneAsync(
        string bookId, 
        bool retrieveDeleted = false, 
        CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetOneAsync(bookId, retrieveDeleted, cancellationToken);

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

    public async Task UpdateAsync(
        string bookId, 
        Book book, 
        CancellationToken cancellationToken = default)
    {
        var bookToUpdate = await GetOneAsync(bookId, true, cancellationToken);
        EnsureBookIsFound(bookToUpdate);

        await _bookRepository.UpdateAsync(bookId, book, cancellationToken);
    }

    public async Task UpdateQuantityAsync(
        string bookId, 
        int quantity, 
        CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetOneAsync(bookId, true, cancellationToken);
        EnsureBookIsFound(book);

        if (quantity < 0)
        {
            throw new InvalidQuantityToUpdateException();
        }

        await _bookRepository.UpdateQuantityAsync(bookId, quantity, cancellationToken);
    }

    public async Task ReplaceAsync(
        string bookId, 
        Book book, 
        CancellationToken cancellationToken = default)
    {
        var bookToReplace = await _bookRepository.GetOneAsync(bookId, true, cancellationToken);
        EnsureBookIsFound(bookToReplace);

        await _bookRepository.ReplaceAsync(bookId, book, cancellationToken);
    }

    public async Task DeleteAsync(string bookId, CancellationToken cancellationToken = default)
    {
        var book = await GetOneAsync(bookId, true, cancellationToken);
        EnsureBookIsFound(book);

        await _bookRepository.DeleteAsync(bookId, cancellationToken);
    }

    private void EnsureBookIsFound(Book book)
    {
        if (book is null)
        {
            throw new BookNotFoundException();
        }

        if (book.IsDeleted)
        {
            throw new BookAlreadyDeletedException();
        }
    }
}
