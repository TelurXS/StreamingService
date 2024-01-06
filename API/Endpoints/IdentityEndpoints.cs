using System.Security.Claims;
using Carter;

namespace API.Endpoints;

public class IdentityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/identity", GetIdentityName).RequireAuthorization();
    }

    private async Task<IResult> GetIdentityName(ClaimsPrincipal claims)
    {
        return Results.Ok(claims.Identity!.Name);
    }
}