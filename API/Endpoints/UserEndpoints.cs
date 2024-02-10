﻿using API.Extensions;
using Application.Features.TitleLists;
using Application.Features.TitlesLists;
using Application.Features.Users;
using Carter;
using Domain.Entities;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using Domain.Models.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace API.Endpoints;

public class UserEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Manage.Profile, GetUserProfileAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Manage.Profile, UpdateUserProfileAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Manage.Genres, GetGenresFromUserAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Manage.Genres, SetGenresToUserAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.ViewRecords, GetViewRecordsAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Users.RegisterViewRecord, AddViewRecordAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.FavouriteTitles.Route, GetFavouriteTitlesAsync)
			.RequireAuthorization();
		
		app.MapPost(ApiRoutes.Users.FavouriteTitles.RouteWithId, AddTitleToFavouriteAsync)
			.RequireAuthorization();

		app.MapDelete(ApiRoutes.Users.FavouriteTitles.RouteWithId, RemoveTitleFromFavouriteAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.TitlesLists.All, GetTitlesListsFromUserAsync)
			.RequireAuthorization();
	}

	[ProducesResponseType<List<TitlesListResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesListsFromUserAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		var request = new GetTitlesListsByAuthor.Request
		{
			AuthorId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			lists => Results.Ok(lists.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetUserProfileAsync(
	[FromServices] IMediator mediator,
	[FromServices] IResponseMapper mapper,
	ClaimsPrincipal claims)
	{
		var request = new GetUserById.Request
		{
			Id = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			user => Results.Ok(mapper.ToResponse(user)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> UpdateUserProfileAsync(
		[FromBody] UpdateUserProfile.Request request,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims)
	{
		request.Id = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> SetGenresToUserAsync(
		[FromBody] SetFavouriteGenresToUser.Request request,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims)
	{
		request.UserId = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<List<GenreResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetGenresFromUserAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		var request = new GetFavouriteGenresFromUser.Request
		{
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			genres => Results.Ok(genres.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> AddViewRecordAsync(
		[FromRoute] Guid seriesId,
		[FromBody] RegisterViewRecordToUser.Request request,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims)
	{
		request.UserId = claims.GetIdentifier();
		request.SeriesId = seriesId;

		var result = await mediator.Send(request);

		return result.Match(
			success => Results.Ok(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetViewRecordsAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		var request = new GetViewRecordsFromUser.Request
		{
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			viewRecords => Results.Ok(viewRecords.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetFavouriteTitlesAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims
		)
	{
		var request = new GetFavouriteTitlesFromUser.Request
		{
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> AddTitleToFavouriteAsync(
		[FromRoute] Guid titleId,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new AddTitleToUserFavourite.Request
		{
			UserId = claims.GetIdentifier(),
			TitleId = titleId
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
	private static async Task<IResult> RemoveTitleFromFavouriteAsync(
		[FromRoute] Guid titleId,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims
		)
	{
		var request = new RemoveTitleFromUserFavourite.Request
		{
			UserId = claims.GetIdentifier(),
			TitleId = titleId
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
