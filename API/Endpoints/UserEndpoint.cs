using Application.Features.TitlesLists;
using Application.Features.Users;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models.Responses;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Domain.Models.Results;
using Application.Features.Titles;

namespace API.Endpoints;

public class UserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Users.Profile, GetUserProfileAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.Genres, GetGenresFromUserAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.ViewRecords, GetViewRecordsAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.FavouriteTitles, GetFavouriteTitlesAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.TitlesLists, GetTitlesListsFromUserAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.Readers, GetReadersFromUserAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.Followers, GetFollowersFromUserAsync)
			.RequireAuthorization();

        app.MapGet(ApiRoutes.Users.All, GetAllUsersAsync)
            .RequireAuthorization();

		app.MapGet(ApiRoutes.Users.AllByName, GetUsersByNameAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Users.CountAll, GetAllUsersCountAsync)
            .RequireAuthorization();

		app.MapGet(ApiRoutes.Users.CountByName, GetUsersCountByNameAsync)
			.RequireAuthorization();

		app.MapPut(ApiRoutes.Users.Profile, UpdateUserAsync)
			.RequireAuthorization();

        app.MapDelete(ApiRoutes.Users.ById, DeleteUserByIdAsync)
            .RequireAuthorization();
    }

	[ProducesResponseType<List<TitlesListResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesListsFromUserAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesListsByAuthor.Request
		{
			AuthorId = id,
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
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetUserById.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			user => Results.Ok(mapper.ToResponse(user)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<List<GenreResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetGenresFromUserAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetFavouriteGenresFromUser.Request
		{
			UserId = id
		};

		var result = await mediator.Send(request);

		return result.Match(
			genres => Results.Ok(genres.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetViewRecordsAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetViewRecordsFromUser.Request
		{
			UserId = id,
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
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetFavouriteTitlesFromUser.Request
		{
			UserId = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType<List<UserResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetFollowersFromUserAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetFollowersFromUser.Request
		{
			Id = id
		};

		var result = await mediator.Send(request);

		return result.Match(
			users => Results.Ok(users.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType<List<UserResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetReadersFromUserAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetReadersFromUser.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			users => Results.Ok(users.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

    [ProducesResponseType<List<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private async Task<IResult> GetAllUsersAsync(
        [FromServices] IMediator mediator,
        [FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0)
    {
        var request = new GetAllUsers.Request
        {
            Count = count,
			Page = page
        };

        var result = await mediator.Send(request);

        return result.Match(
            users => Results.Ok(users.Select(x => mapper.ToResponse(x))),
            notFound => Results.NotFound(),
            failed => Results.BadRequest()
            );
    }

	[ProducesResponseType<List<UserResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetUsersByNameAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] string name,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0)
	{
		var request = new GetUsersByName.Request
		{
			Name = name,
			Count = count,
			Page = page
		};

		var result = await mediator.Send(request);

		return result.Match(
			users => Results.Ok(users.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private async Task<int> GetAllUsersCountAsync(
        [FromServices] IMediator mediator
		)
    {
        var request = new GetAllUsersCount.Request
        {
        };

        var result = await mediator.Send(request);

		return result;
    }
	
	[ProducesResponseType<int>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private async Task<int> GetUsersCountByNameAsync(
        [FromServices] IMediator mediator,
		[FromQuery] string name
		)
    {
        var request = new GetUsersCountByName.Request
        {
			Name = name
        };

        var result = await mediator.Send(request);

		return result;
    }

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> UpdateUserAsync(
		[FromRoute] Guid id,
		[FromBody] UpdateUser.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		request.Id = id;

		var result = await mediator.Send(request);

		return result.Match(
			user => Results.Ok(mapper.ToResponse(user)),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> DeleteUserByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator)
    {
        var request = new DeleteUserById.Request { Id = id };

        var result = await mediator.Send(request);

        return result.Match(
            title => Results.Ok(),
            notFound => Results.NotFound(),
            failed => Results.BadRequest());
    }
}
