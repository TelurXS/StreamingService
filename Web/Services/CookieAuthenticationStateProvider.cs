using Domain.Models;
using Domain.Models.Requests;
using Domain.Models.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Web.Services;

public sealed class CookieAuthenticationStateProvider : AuthenticationStateProvider
{
    public CookieAuthenticationStateProvider(
		HttpClient client, 
		ILogger<CookieAuthenticationStateProvider> logger)
    {
		Client = client;
		Logger = logger;
	}

	private HttpClient Client { get; }
	private ILogger<CookieAuthenticationStateProvider> Logger { get; }

	private static ClaimsPrincipal Anonymous => new ClaimsPrincipal(new ClaimsIdentity());

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		try
		{
			var response = await Client.GetAsync(ApiRoutes.User);

			if (response.IsSuccessStatusCode is false)
				return new AuthenticationState(Anonymous);

			var user = await response.Content.ReadFromJsonAsync<List<ClaimResponse>>();

			if (user is null || user.Count <= 0)
			{
				return new AuthenticationState(Anonymous);
			}

			var claims = user.Select(x => new Claim(x.Key, x.Value));

			Console.WriteLine("Authorizing: " + claims.FirstOrDefault()?.Value);

			return new AuthenticationState(
				new ClaimsPrincipal(
					new ClaimsIdentity(
						claims,
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
		var response = await Client
			.PostAsJsonAsync(
			$"{ApiRoutes.Login}?useCookies=true&useSessionCookies={!request.RememberMe}", 
			request);

		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		return response.IsSuccessStatusCode;
	}

	public async Task<bool> LogoutAsync()
	{
		var response = await Client.PostAsJsonAsync(ApiRoutes.Logout, new { });
		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		return response.IsSuccessStatusCode;
	}
}

