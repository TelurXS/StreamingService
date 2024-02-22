using Application.Features.Serieses;
using Azure;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public class SeriesService : ISeriesService
{
	public SeriesService(HttpClient client)
	{
		Client = client;
	}

	private HttpClient Client { get; }


	public async Task<int> CountAsync()
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Series.CountAll);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public Task<CreateResult<Series>> CreateAsync(CreateSeries.Request value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteAsync(Series value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<GetAllResult<Series>> FindAllAsync(int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Series.All + $"?count={count}&page={page}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Series>>();

			return new Failed();
		}
		catch (Exception ex) 
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetResult<Series>> FindByIdAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Series.All + $"/{id}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Series>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Series>> UpdateAsync(Guid id, UpdateSeries.Request value)
	{
		try
		{
			var response = await Client
				.PutAsJsonAsync(ApiRoutes.Series.All + $"/{id}", value);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Series>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
