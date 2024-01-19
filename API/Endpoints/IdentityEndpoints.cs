using System.Security.Claims;
using Carter;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Endpoints;

public class IdentityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/identity", GetIdentityName)
            .RequireAuthorization();

        app.MapGet("/for-admin", ForAdminOnly)
            .RequireAuthorization(x => x.RequireRole(Role.Admin));

        app.MapIdentityApi<User>();

        app.MapPost("/logout", Logout);

        app.MapGet("/user", GetUserClaims)
            .RequireAuthorization();
    }

    private IResult GetIdentityName(ClaimsPrincipal claims)
    {
        return Results.Ok(claims.Identity!.Name);
    }

	private IResult ForAdminOnly(ClaimsPrincipal claims)
	{
		return Results.Ok(claims.Identity!.Name);
	}

    private IResult GetUserClaims(HttpContext context)
    {
        return Results.Ok(context.User.Claims
            .Select(x => KeyValuePair.Create(x.Type, x.Value)));
    }

    private IResult Logout(HttpContext context)
    {
        return Results.SignOut(authenticationSchemes: [IdentityConstants.ApplicationScheme]);
    }
}