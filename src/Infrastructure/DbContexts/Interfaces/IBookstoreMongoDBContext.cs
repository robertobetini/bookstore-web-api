using MongoDB.Driver;

namespace Infrastructure.DbContexts.Interfaces;

public interface IBookstoreMongoDBContext
{
    public IMongoDatabase Database { get; init; }
}
