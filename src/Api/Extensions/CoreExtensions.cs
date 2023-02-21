using Core.Providers;
using Core.Providers.Interfaces;
using Core.Services;
using Core.Services.Interfaces;

namespace Api.Extensions;

public static class CoreExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IEnvironmentVariableProvider, EnvironmentVariableProvider>()
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IHashService, SHA256HashService>()
            .AddSingleton<ITokenService, JwtTokenService>();
    }
}
