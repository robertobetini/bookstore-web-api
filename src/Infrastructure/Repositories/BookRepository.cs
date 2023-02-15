using Core.Entities;
using Core.Entities.Enums;
using Core.Repositories.Interfaces;
using Infrastructure.Adapters;
using Infrastructure.DbContexts.Interfaces;
using Infrastructure.DbEntities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<BookMongoDB> _collection;
    private readonly FilterDefinitionBuilder<BookMongoDB> _bookFilter = Builders<BookMongoDB>.Filter;
    private readonly UpdateDefinitionBuilder<BookMongoDB> _bookUpdate = Builders<BookMongoDB>.Update;

    public BookRepository(IBookstoreMongoDBContext context)
    {
        _collection = context.Database.GetCollection<BookMongoDB>("books");
    }

    public async Task<IEnumerable<Book>> GetManyAsync(CancellationToken cancellationToken = default)
    {
        var filter = GetIgnoreDeletedFilter();
        var result = (await _collection
            .FindAsync(filter, cancellationToken: cancellationToken))
            .ToEnumerable();

        return BookAdapter.ToDomainEntities(result);
    }

    public async Task<Book> GetOneAsync(string bookId, CancellationToken cancellationToken = default)
    {
        var filter = _bookFilter.Eq(b => b.Id, bookId) & GetIgnoreDeletedFilter();
        var result = await _collection
            .Find(filter)
            .FirstOrDefaultAsync(cancellationToken);

        return BookAdapter.ToDomainEntity(result);
    }

    public async Task<string> Create(Book book, CancellationToken cancellationToken = default)
    {
        var bookMongoDB = BookAdapter.ToMongoDBEntity(book);
        await _collection.InsertOneAsync(bookMongoDB, cancellationToken: cancellationToken);
        return bookMongoDB.Id;
    }

    public async Task UpdateAsync(string bookId, Book book, CancellationToken cancellationToken = default)
    {
        var filter = _bookFilter.Eq(b => b.Id, bookId) & GetIgnoreDeletedFilter();
        var updates = new List<UpdateDefinition<BookMongoDB>>();

        if (book.Title is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Title, book.Title));
        }

        if (book.Author is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Author, book.Author));
        }

        if (book.Edition is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Edition, book.Edition));
        }

        if (book.Language is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Language, book.Language));
        }

        if (book.Publisher is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Publisher, book.Publisher));
        }

        if (book.Pages is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Pages, book.Pages));
        }

        if (book.Price is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Price, book.Price));
        }

        if (book.Year is not null)
        {
            updates.Add(_bookUpdate.Set(b => b.Year, book.Year));
        }

        if (book.Preservation is not null && book.Preservation is not BookPreservation.Undefined)
        {
            updates.Add(_bookUpdate.Set(b => b.Preservation, book.Preservation));
        }

        if (!updates.Any())
        {
            return;
        }

        var finalUpdate = _bookUpdate.Combine(updates);

        _ = await _collection.UpdateOneAsync(filter, finalUpdate, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string bookId, CancellationToken cancellationToken = default)
    {
        var filter = _bookFilter.Eq(b => b.Id, bookId);
        var update = _bookUpdate.Set(b => b.IsDeleted, true);
        _ = await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    private FilterDefinition<BookMongoDB> GetIgnoreDeletedFilter() => _bookFilter.Ne(b => b.IsDeleted, false);
}
