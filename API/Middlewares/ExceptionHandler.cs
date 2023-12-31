﻿using Microsoft.AspNetCore.Diagnostics;

namespace API.Middlewares;

public sealed class ExceptionHandler : IMiddleware
{
    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        Logger = logger;
    }
    
    private ILogger<ExceptionHandler> Logger { get; }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                Message = e.Message 
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}