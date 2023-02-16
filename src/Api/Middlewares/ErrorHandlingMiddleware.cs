using Api.Factories;
using Core.Exceptions;
using System.Net;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException exception)
        {
            await HandleError(context, exception, HttpStatusCode.BadRequest);
            _logger.LogWarning(exception, exception.Message);
        }
        catch (ResourceNotFoundException exception)
        {
            await HandleError(context, exception, HttpStatusCode.NotFound);
            _logger.LogWarning(exception, exception.Message);
        }
        catch (ResourceAlreadyDeletedException exception)
        {
            await HandleError(context, exception, HttpStatusCode.Gone);
            _logger.LogWarning(exception, exception.Message);
        }
        catch (Exception exception)
        {
            await HandleError(context, exception, HttpStatusCode.InternalServerError);
            _logger.LogError(exception, exception.Message);
        }
    }

    private async Task HandleError(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;

        var problemDetails = ProblemDetailsFactory.Create(context, exception, statusCode);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
