using Core;
using Core.Providers;
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
            .AddMySQLContext()
            .AddMongoDbContext()
            .AddMongoDbRepositories()
            .AddMySQLRepositories();
    }

    private static IServiceCollection AddMySQLContext(this IServiceCollection services)
    {
        var userMySQLConnectionKey = Constants.EnvironmentVariableKeys.USER_MYSQL_CONNECTION;
        var mysqlConnectionString = EnvironmentVariableProvider.GetValue(userMySQLConnectionKey);

        return services.AddSingleton<IUserMySQLContext>(
            provider => new UserMySQLContext(mysqlConnectionString));
    }

    private static IServiceCollection AddMongoDbContext(this IServiceCollection services)
    {
        var bookstoreMongoDBConnectionKey = Constants.EnvironmentVariableKeys.BOOKSTORE_MONGODB_CONNECTION;
        var mongoConnectionString = EnvironmentVariableProvider.GetValue(bookstoreMongoDBConnectionKey);

        return services.AddSingleton<IBookstoreMongoDBContext>(
            provider => new BookstoreMongoDBContext(mongoConnectionString));
    }

    private static IServiceCollection AddMySQLRepositories(this IServiceCollection services)
    {
        return services.AddSingleton<IUserRepository, UserRepository>();
    }

    private static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IBookPhotoRepository, BookPhotoRepository>();
    }
}
