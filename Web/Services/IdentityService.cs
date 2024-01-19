using Domain.Interfaces.Services;
using Domain.Models.Requests;
using System.Net.Http.Json;

namespace Web.Services;

public sealed class IdentityService : IIdentityService
{
    public IdentityService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<string> RegisterAsync(RegisterRequest request)
	{
		var response = await Client.PostAsJsonAsync("/register", request);

		if (response.IsSuccessStatusCode)
			return "Success";

		return await response.Content.ReadAsStringAsync();
	}

	public async Task<bool> ConfirmEmailAsync(string userId, string code)
	{
		var response = await Client
			.GetAsync($"/confirmEmail?userId={userId}&code={code}");

		return response.IsSuccessStatusCode;
	}
}
