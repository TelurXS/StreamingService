using API.Extensions;
using Application.Features.Notifications;
using Carter;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public sealed class NotificationsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost(ApiRoutes.Notifications.InviteById, SendInviteToUserAsync);
	}

	private static async Task<IResult> SendInviteToUserAsync(
		[FromRoute] Guid id,
		[FromBody] SendSessionInviteNotification.Request request,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		request.ReceiverId = id;
		request.AuthorId = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}
}
