using Microsoft.AspNetCore.Diagnostics;

namespace API.Middlewares;

public sealed class ExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                Message = e.Message 
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}