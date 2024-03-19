using API.Extensions;
using Application.Features.Subscriptions;
using Carter;
using Domain.Entities;
using Domain.Interfaces.Mappings;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public sealed class PaymentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Payment.CaptureOrder, CaptureOrderAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Payment.CreateOrder, CreateOrderAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Subscriptions.Cancel, CancelSubscriptionAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Subscriptions.All, GetAllSubscriptionsAsync);

		app.MapGet(ApiRoutes.Subscriptions.ById, GetSubscriptionByIdAsync);

		app.MapGet(ApiRoutes.Subscriptions.ByName, GetSubscriptionByNameAsync);
	}

	[ProducesResponseType<Subscription>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetSubscriptionByIdAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetSubscriptionById.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			subscription => Results.Ok(mapper.ToResponse(subscription)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest(failed.Errors)
			);
	}

	[ProducesResponseType<Subscription>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetSubscriptionByNameAsync(
		[FromRoute] string name,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetSubscriptionByName.Request
		{
			Name = name,
		};

		var result = await mediator.Send(request);

		return result.Match(
			subscription => Results.Ok(mapper.ToResponse(subscription)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest(failed.Errors)
			);
	}

	[ProducesResponseType<List<Subscription>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetAllSubscriptionsAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetAllSubscriptions.Request
		{
		};

		var result = await mediator.Send(request);

		return result.Match(
			subscriptions => Results.Ok(subscriptions.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest(failed.Errors)
			);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
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


	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> CancelSubscriptionAsync(
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new CancelSubscriptionToUser.Request
		{
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			subscription => Results.Ok(),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}
}
