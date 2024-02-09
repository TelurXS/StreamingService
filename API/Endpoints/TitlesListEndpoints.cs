using API.Extensions;
using Application.Features.TitlesLists;
using Carter;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public class TitlesListEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost(ApiRoutes.TitlesLists.AddTitleToList, AddTitleToListAsync)
			.RequireAuthorization();

		app.MapDelete(ApiRoutes.TitlesLists.RemoveTitleFromList, RemoveTitleFromListAsync)
			.RequireAuthorization();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> AddTitleToListAsync(
		[FromRoute] Guid titleId,
		[FromRoute] Guid listId,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new AddTitleToList.Request
		{
			TitleId = titleId,
			TitlesListId = listId,
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> RemoveTitleFromListAsync(
		[FromRoute] Guid titleId,
		[FromRoute] Guid listId,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new RemoveTitleFromList.Request
		{
			TitleId = titleId,
			TitlesListId = listId,
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}
}
