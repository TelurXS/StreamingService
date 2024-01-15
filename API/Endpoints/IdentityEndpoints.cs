using System.Security.Claims;
using Carter;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class IdentityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/identity", GetIdentityName)
            .RequireAuthorization();

        app.MapGet("/for-admin", ForAdminOnly)
            .RequireAuthorization(x => x.RequireRole(Role.Admin));

        app.MapGet("/user", GetUserClaimsAsync)
            .RequireAuthorization();

        app.MapIdentityApi<User>();
	}

    private IResult GetIdentityName(ClaimsPrincipal claims)
    {
        return Results.Ok(claims.Identity!.Name);
    }

	private IResult ForAdminOnly(ClaimsPrincipal claims)
	{
		return Results.Ok(claims.Identity!.Name);
	}

    private IResult GetUserClaimsAsync(HttpContext context)
    {
        return Results.Ok(context.User.Claims
            .Select(x => KeyValuePair.Create(x.Type, x.Value)));
    }
}