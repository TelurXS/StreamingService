using Application.Extensions;
using Application.Features.Accounts;
using Domain.Entities;
using Domain.Models.Results;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName!
        .Split('.')
        .Last()
        .Replace("+", ""));
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPost("/account", async (
    [FromBody] CreateAccount.Request request,
    [FromServices] IMediator mediator, 
    CancellationToken token) =>
{
    var result = await mediator.Send(request, token);
    
    return result.Match(
        account => Results.Ok(account),
        validationFailed => Results.BadRequest(validationFailed),
        failed => Results.BadRequest(failed));
});

app.MapPut("/account", async (
    [FromBody] UpdateAccount.Request request,
    [FromServices] IMediator mediator, 
    CancellationToken token) =>
{
    var result = await mediator.Send(request, token);
    
    return result.Match(
        account => Results.Ok(account),
        notFound => Results.NotFound(), 
        validationFailed => Results.BadRequest(validationFailed),
        failed => Results.BadRequest(failed));
});

app.Run();