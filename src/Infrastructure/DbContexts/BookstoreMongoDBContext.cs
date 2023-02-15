using Infrastructure.DbContexts.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.DbContexts;

public class BookstoreMongoDBContext : IBookstoreMongoDBContext
{
	private const string DATABASE_NAME = "bookstore_db";
	private readonly IMongoClient _mongoClient;

	public IMongoDatabase Database { get; init; }

	public BookstoreMongoDBContext(string connectionString)
	{
		_mongoClient = new MongoClient(connectionString);
		Database = _mongoClient.GetDatabase(DATABASE_NAME);
	}
}
