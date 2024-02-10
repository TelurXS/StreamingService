using API.Extensions;
using Application.Features.TitleLists;
using Application.Features.TitlesLists;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace API.Endpoints;

public class TitlesListEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.TitlesLists.ById, GetTitlesListByIdAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.TitlesLists.Route, CreateTitlesListAsync)
			.RequireAuthorization();

		app.MapPut(ApiRoutes.TitlesLists.ById, UpdateTitlesListAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.TitlesLists.AddTitleToList, AddTitleToListAsync)
			.RequireAuthorization();

		app.MapDelete(ApiRoutes.TitlesLists.RemoveTitleFromList, RemoveTitleFromListAsync)
			.RequireAuthorization();

		app.MapDelete(ApiRoutes.TitlesLists.ById, DeleteTitlesListByIdAsync)
			.RequireAuthorization();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetTitlesListByIdAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesListById.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			list => Results.Ok(mapper.ToResponse(list)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<TitlesListResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> CreateTitlesListAsync(
		[FromBody] CreateTitlesList.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		request.UserId = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			list => Results.Ok(mapper.ToResponse(list)),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<TitlesListResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> UpdateTitlesListAsync(
		[FromRoute] Guid id,
		[FromBody] UpdateTitlesList.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		request.Id = id;
		request.UserId = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			list => Results.Ok(mapper.ToResponse(list)),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
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

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> DeleteTitlesListByIdAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator)
	{
		var request = new DeleteTitlesListById.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}
}
