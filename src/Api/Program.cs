using Api.Extensions;
using Core.Repositories.Interfaces;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.DbContexts;
using Infrastructure.DbContexts.Interfaces;
using Infrastructure.Repositories;
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

app.UseAuthorization();

app.MapControllers();

app.UseErrorHandlingMiddleware();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services
        .AddSingleton<IBookService, BookService>();

    var mongoConnectionString = Environment.GetEnvironmentVariable("BookstoreMongoDBConnection");
    services
        .AddSingleton<IBookRepository, BookRepository>()
        .AddSingleton<IBookstoreMongoDBContext, BookstoreMongoDBContext>(
            provider => new BookstoreMongoDBContext(mongoConnectionString));
}
