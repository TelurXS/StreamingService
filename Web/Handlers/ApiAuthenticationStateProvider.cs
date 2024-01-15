using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Web.Handlers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    public ApiAuthenticationStateProvider(
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor contextAccessor,
        ILogger<ApiAuthenticationStateProvider> logger)
    {
        Client = httpClientFactory.CreateClient("api");
        ContextAccessor = contextAccessor;
        Logger = logger;
    }

    private HttpClient Client { get; }
    private IHttpContextAccessor ContextAccessor { get; }
    private ILogger<ApiAuthenticationStateProvider> Logger { get; }

    private static ClaimsPrincipal Anonymous => new ClaimsPrincipal(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var claims = await Client
                .GetFromJsonAsync<List<KeyValuePair<string, string>>>("/user");

            if (claims is not { Count: > 0 })
                return new AuthenticationState(Anonymous);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        claims.Select(x => new Claim(x.Key, x.Value)),
                        IdentityConstants.ApplicationScheme
                        )));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return new AuthenticationState(Anonymous);
        }
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var response = await Client.PostAsJsonAsync("/login?useCookies=true", request);

        if (response.IsSuccessStatusCode is false)
            return false;

        var context = ContextAccessor.HttpContext;

        if (context is null)
            return false;

        var header = response.Headers
            .GetValues(HeaderNames.SetCookie)
            .FirstOrDefault();

        if (header is null)
            return false;

        context.Response.Headers.Append(HeaderNames.SetCookie, header);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return true;
    }

    public async Task LogoutAsync(LoginRequest request)
    {
        var context = ContextAccessor.HttpContext;

        if (context is null)
            return;

        await context.SignOutAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
