using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Factories;

public static class ProblemDetailsFactory
{
    private const string PROBLEM_DETAILS_TYPE = "about:blank";

    public static ProblemDetails Create(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        return new ProblemDetails
        {
            Title = exception.Message,
            Status = (int?)statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path,
            Type = PROBLEM_DETAILS_TYPE
        };
    }

}
