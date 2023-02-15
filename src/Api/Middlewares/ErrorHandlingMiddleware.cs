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
		catch (ResourceNotFoundException exception)
		{
			HandleError(context, exception, HttpStatusCode.NotFound);
			_logger.LogWarning(exception, exception.Message);
		}
        catch (ResourceAlreadyDeletedException exception)
        {
            HandleError(context, exception, HttpStatusCode.Gone);
            _logger.LogWarning(exception, exception.Message);
        }
        catch (Exception exception)
		{
            HandleError(context, exception, HttpStatusCode.InternalServerError);
            _logger.LogError(exception, exception.Message);
		}
	}

	private void HandleError(HttpContext context, Exception exception, HttpStatusCode statusCode)
	{
		context.Response.StatusCode = (int)statusCode;
	}
}
