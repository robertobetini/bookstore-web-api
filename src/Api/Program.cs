using Api.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddCoreServices()
        .AddRepositories()
        .AddJwtTokenAuthentication();
}
