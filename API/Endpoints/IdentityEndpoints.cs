using System.Net.Mime;
using System.Security.Claims;
using System.Security.Policy;
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
using Microsoft.DotNet.Scaffolding.Shared;

namespace API.Endpoints;

public class IdentityEndpoints : CarterModule
{
    public IdentityEndpoints()
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiRoutes.Base).MapIdentityApi<User>();

        app.MapGet(ApiRoutes.ExternalLogin, ExternalLoginAsync);

        app.MapGet(ApiRoutes.ExternalLoginCallback, ExternalLoginCallbackAsync);

        app.MapPost(ApiRoutes.Logout, Logout)
            .RequireAuthorization();

        app.MapGet(ApiRoutes.User, GetUserClaims)
            .RequireAuthorization();
    }

    private static async Task<IResult> ExternalLoginCallbackAsync(
        [FromServices] SignInManager<User> signInManager,
        [FromServices] UserManager<User> userManager,
        [FromQuery] string? returnUrl = null,
        [FromQuery] string? remoteError = null
        )
    {
        returnUrl = returnUrl ?? "~/";

        if (remoteError is not null)
            return Results.BadRequest(remoteError);

        var info = await signInManager.GetExternalLoginInfoAsync();

        if (info is null)
            return Results.BadRequest("Error loading external login information.");

        var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
            info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
            return Results.LocalRedirect(returnUrl);

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);

        if (name is null || email is null)
            return Results.BadRequest();

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            return Results.BadRequest();

        user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            FirstName = name,
            UserName = name,
            Email = email,
        };

        await userManager.CreateAsync(user);

        await userManager.AddLoginAsync(user, info);
        await signInManager.SignInAsync(user, isPersistent: false);

        return Results.LocalRedirect(returnUrl);
    }


    private static IResult ExternalLoginAsync(
        [FromQuery] string provider,
        [FromQuery] string returnUrl,
        [FromServices] SignInManager<User> signInManager)
    {
        var redirectUrl = $"{ApiRoutes.ExternalLoginCallback}?returnUrl={returnUrl}";

        var properties =
            signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Results.Challenge(properties);
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
    private static async Task<IResult> Logout(
        [FromServices] SignInManager<User> signInManager
        )
    {
        return Results.SignOut(authenticationSchemes: [IdentityConstants.ApplicationScheme]);
    }
}