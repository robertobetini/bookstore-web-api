using Core.Entities;

namespace Core.Services.Interfaces;

public interface IBookService
{
    Task<Book> GetOneAsync(string bookId, bool retrieveDeleted = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetManyAsync(bool retrieveDeleted = false, CancellationToken cancellationToken = default);
    Task<string> CreateAsync(Book book, CancellationToken cancellationToken = default);
    Task UpdateAsync(string bookId, Book book, CancellationToken cancellationToken = default);
    Task UpdateQuantityAsync(string bookId, int quantity, CancellationToken cancellationToken = default);
    Task ReplaceAsync(string bookId, Book book, CancellationToken cancellationToken = default);
    Task DeleteAsync(string bookId, CancellationToken cancellationToken = default);

}
