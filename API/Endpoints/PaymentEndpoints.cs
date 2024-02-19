using API.Extensions;
using API.Services;
using Application.Features.Subscriptions;
using Carter;
using Domain.Entities;
using Domain.Interfaces.Services;
using MailKit.Search;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public sealed class PaymentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/payment", CaptureOrderAsync)
			.RequireAuthorization();

		app.MapPost("/api/payment/{subscription}", CreateOrderAsync)
			.RequireAuthorization();
	}

	private static async Task<IResult> CaptureOrderAsync(
		[FromQuery] string orderId,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new CaptureSubscriptionOrder.Request
		{
			OrderId = orderId,
			UserId = claims.GetIdentifier(),
        };

        var result = await mediator.Send(request);

        return result.Match(
			response => Results.Ok(response),
            notFound => Results.NotFound(),
            invalid => Results.BadRequest(),
            failed => Results.BadRequest(failed.Errors)
            );
	}

	private static async Task<IResult> CreateOrderAsync(
		[FromRoute] string subscription,
		[FromServices] IMediator mediator
		)
	{
		var request = new CreateSubscriptionOrder.Request
		{
			SubscriptionName = subscription,
		};

		var result = await mediator.Send(request);

        return result.Match(
            order => Results.Ok(order),
            notFound => Results.NotFound(),
            failed => Results.BadRequest()
            );
    }
}
