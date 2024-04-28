using System.Net;

namespace Pollr2.API.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // TODO: more descriptive log message
            logger.LogError(ex, "An unexpected error occurred");

            await context.Response.WriteAsync("An unexpected error occurred");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
