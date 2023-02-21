using Api.Extensions;
using Core.Providers;
using Core.Providers.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.DbContexts.MongoDB;
using Infrastructure.DbContexts.MongoDB.Interfaces;
using Infrastructure.DbContexts.MySQL;
using Infrastructure.DbContexts.MySQL.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Tls;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseErrorHandlingMiddleware();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    // Core
    services
        .AddSingleton<IEnvironmentVariableProvider, EnvironmentVariableProvider>()
        .AddSingleton<IBookService, BookService>()
        .AddSingleton<IUserService, UserService>()
        .AddSingleton<IHashService, SHA256HashService>()
        .AddSingleton<ITokenService, JwtTokenService>();

    // Infrastructure
    var mongoConnectionString = Environment.GetEnvironmentVariable("BookstoreMongoDBConnection");
    var mysqlConnectionString = Environment.GetEnvironmentVariable("UserMySQLConnection");
    services
        .AddSingleton<IBookRepository, BookRepository>()
        .AddSingleton<IUserRepository, UserRepository>()
        .AddSingleton<IBookstoreMongoDBContext>(
            provider => new BookstoreMongoDBContext(mongoConnectionString))
        .AddSingleton<IUserMySQLContext>(
            provider => new UserMySQLContext(mysqlConnectionString));

    // Authentication Config
    var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer");
    var jwtAudience = Environment.GetEnvironmentVariable("JwtAudience");
    var jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecret");
    var secretBytes = Encoding.UTF8.GetBytes(jwtSecretKey);
    services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
            };
        });
}
