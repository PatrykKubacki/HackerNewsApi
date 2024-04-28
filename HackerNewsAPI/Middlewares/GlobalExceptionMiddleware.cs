using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HackerNewsAPI.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = ex.Message,
            };

            if(ex is HttpRequestException || ex is TaskCanceledException)
            {
                problem.Title = "HttpClient error";
                problem.Type = "HttpClient error";
            }

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

/*
  Global handler for handling response construction in case of an exception, and log errors 
  can be expanded with customException.
 */