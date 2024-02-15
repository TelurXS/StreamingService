using Domain.Entities;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class RateService : IRateService
{
    public RateService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<GetResult<Rate>> GetRateByTitleAsync(Title title)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Rates.Route + $"/{title.Id}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Rate>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
