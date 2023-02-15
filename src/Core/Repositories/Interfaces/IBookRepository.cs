using Core.Entities;

namespace Core.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> GetOneAsync(string bookId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetManyAsync(CancellationToken cancellationToken = default);
    Task<string> Create(Book book, CancellationToken cancellationToken = default);
    Task UpdateAsync(string bookId, Book book, CancellationToken cancellationToken = default);
    Task<Book> DeleteAsync(string bookId, CancellationToken cancellationToken = default);
}
