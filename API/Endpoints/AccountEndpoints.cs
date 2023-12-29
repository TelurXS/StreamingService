using System.Net.Mime;
using Application.Features.Accounts;
using Carter;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public sealed class AccountEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/{id:guid}", GetAccountByIdAsync);
        group.MapPost("/", CreateAccountAsync);
        group.MapPut("/{id:guid}", UpdateAccountAsync);
        group.MapDelete("/{id:guid}", DeleteAccountAsync);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> GetAccountByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator)
    {
        var request = new GetAccountById.Request
        {
            Id = id,
        };

        var result = await mediator.Send(request);

        return result.Match(
            account => Results.Ok(account),
            notFound => Results.NotFound(),
            failed => Results.BadRequest(failed));
    }
    
    
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> CreateAccountAsync(
        [FromBody] CreateAccount.Request request,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(request);

        return result.Match(
            account => Results.Ok(account),
            validationFailed => Results.BadRequest(validationFailed),
            failed => Results.BadRequest(failed));
    }

    
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> UpdateAccountAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateAccount.Request request,
        [FromServices] IMediator mediator)
    {
        request.Id = id;

        var result = await mediator.Send(request);

        return result.Match(
            account => Results.Ok(account),
            notFound => Results.NotFound(),
            validationFailed => Results.BadRequest(validationFailed),
            failed => Results.BadRequest(failed));
    }
    
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> DeleteAccountAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator)
    {
        var request = new DeleteAccountById.Request
        {
            Id = id,
        };

        var result = await mediator.Send(request);

        return result.Match(
            success => Results.Ok(),
            notFound => Results.NotFound(),
            failed => Results.BadRequest(failed));
    }
}