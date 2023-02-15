using Core.Entities;
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

    public BookRepository(IBookstoreMongoDBContext context)
    {
        _collection = context.Database.GetCollection<BookMongoDB>("books") ;
    }

    public async Task<IEnumerable<Book>> GetMany()
    {
        var result = (await _collection
            .FindAsync(_bookFilter.Empty))
            .ToEnumerable();

        return BookAdapter.ToDomainEntities(result);
    }

    public async Task<Book> GetOne()
    {
        var result = await _collection
            .Find(_bookFilter.Empty)
            .FirstOrDefaultAsync();

        return BookAdapter.ToDomainEntity(result);
    }
}
