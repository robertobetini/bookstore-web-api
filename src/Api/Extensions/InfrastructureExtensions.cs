using Core.Repositories.Interfaces;
using Infrastructure.DbContexts.MongoDB;
using Infrastructure.DbContexts.MongoDB.Interfaces;
using Infrastructure.DbContexts.MySQL;
using Infrastructure.DbContexts.MySQL.Interfaces;
using Infrastructure.Repositories;

namespace Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        return services
            .AddBookRepository()
            .AddUserRepository();
    }

    private static IServiceCollection AddUserRepository(this IServiceCollection services)
    {

        var mysqlConnectionString = Environment.GetEnvironmentVariable("UserMySQLConnection");

        return services
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IUserMySQLContext>(
                provider => new UserMySQLContext(mysqlConnectionString));
    }

    private static IServiceCollection AddBookRepository(this IServiceCollection services)
    {
        var mongoConnectionString = Environment.GetEnvironmentVariable("BookstoreMongoDBConnection");

        return services
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IBookstoreMongoDBContext>(
                provider => new BookstoreMongoDBContext(mongoConnectionString));
    }
}
