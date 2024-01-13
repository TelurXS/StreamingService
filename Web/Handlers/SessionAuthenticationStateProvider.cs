using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Web.Interfaces;

namespace Web.Handlers;

public class SessionAuthenticationStateProvider : AuthenticationStateProvider
{
	public SessionAuthenticationStateProvider(IAuthenticationService authenticationService)
	{
		AuthenticationService = authenticationService;
	}

	private IAuthenticationService AuthenticationService { get; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var token = await AuthenticationService.GetTokenAsync();

		if (string.IsNullOrEmpty(token))
			return new AuthenticationState(new ClaimsPrincipal());

		return new AuthenticationState(new ClaimsPrincipal());
	}
}
