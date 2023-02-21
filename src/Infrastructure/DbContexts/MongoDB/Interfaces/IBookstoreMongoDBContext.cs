using MongoDB.Driver;

namespace Infrastructure.DbContexts.MongoDB.Interfaces;

public interface IBookstoreMongoDBContext
{
    public IMongoDatabase Database { get; init; }
}
