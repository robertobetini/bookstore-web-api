using Core;
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
        var userMySQLConnectionKey = Constants.EnvironmentVariableKeys.USER_MYSQL_CONNECTION;
        var mysqlConnectionString = Environment.GetEnvironmentVariable(userMySQLConnectionKey);

        return services
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IUserMySQLContext>(provider => new UserMySQLContext(mysqlConnectionString));
    }

    private static IServiceCollection AddBookRepository(this IServiceCollection services)
    {
        var bookstoreMongoDBConnectionKey = Constants.EnvironmentVariableKeys.BOOKSTORE_MONGODB_CONNECTION;
        var mongoConnectionString = Environment.GetEnvironmentVariable(bookstoreMongoDBConnectionKey);

        return services
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IBookstoreMongoDBContext>(
                provider => new BookstoreMongoDBContext(mongoConnectionString));
    }
}
