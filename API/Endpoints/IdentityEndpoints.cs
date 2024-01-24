using System.Net.Mime;
using System.Security.Claims;
using API.Extensions;
using Application.Features.Users;
using Carter;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class IdentityEndpoints : CarterModule
{
    public IdentityEndpoints()
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
		app.MapGroup(ApiRoutes.Base).MapIdentityApi<User>();

		app.MapPost(ApiRoutes.Logout, Logout)
            .RequireAuthorization();

		app.MapGet(ApiRoutes.User, GetUserClaims)
            .RequireAuthorization();

		app.MapGet(ApiRoutes.Manage.Profile, GetUserProfileAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Manage.Profile, UpdateUserProfileAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Manage.Genres, GetGenresFromUserAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Manage.Genres, SetGenresToUserAsync)
            .RequireAuthorization();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	private static IResult GetUserClaims(HttpContext context)
    {
        return Results.Ok(context.User.Claims
            .Select(x => KeyValuePair.Create(x.Type, x.Value)));
    }

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	private static IResult Logout(HttpContext context)
    {
        return Results.SignOut(authenticationSchemes: [IdentityConstants.ApplicationScheme]);
    }

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetUserProfileAsync(
	[FromServices] IMediator mediator,
	ClaimsPrincipal claims)
	{
		var request = new GetUserById.Request
		{
			Id = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			user => Results.Ok(user),
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

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetGenresFromUserAsync(
		[FromServices] IMediator mediator,
		ClaimsPrincipal claims)
	{
		var request = new GetFavouriteGenresFromUser.Request
		{
			UserId = claims.GetIdentifier(),
		};
		
		var result = await mediator.Send(request);

		return result.Match(
			genres => Results.Ok(genres),
			notFound => Results.NotFound(),
			failed => Results.BadRequest());
	}

}