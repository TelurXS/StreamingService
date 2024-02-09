using System.Net.Mime;
using System.Security.Claims;
using API.Extensions;
using Application.Features.Users;
using Carter;
using Domain.Entities;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
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
}