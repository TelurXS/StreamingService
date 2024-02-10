using Domain.Entities;
using Domain.Models.Results;
using Domain.Models;
using Domain.Models.Results.Unions;
using Web.Interfaces;
using System.Net.Http.Json;
using Application.Features.TitleLists;
using Application.Features.TitlesLists;

namespace Web.Services;

public class TitlesListService : ITitlesListService
{
	public TitlesListService(HttpClient client)
	{
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<GetResult<TitlesList>> GetById(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.TitlesLists.Route + $"/{id}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<TitlesList>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> AddTitleToListAsync(Guid listId, Title title)
	{
		try
		{
			var response = await Client
				.PostAsync(ApiRoutes.TitlesLists.Route + $"/{listId}/{title.Id}", default);

			if (response.IsSuccessStatusCode)
				return new Success();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> RemoveTitleFromListAsync(Guid listId, Title title)
	{
		try
		{
			var response = await Client
				.DeleteAsync(ApiRoutes.TitlesLists.Route + $"/{listId}/{title.Id}", default);

			if (response.IsSuccessStatusCode)
				return new Success();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<CreateResult<TitlesList>> CreateAsync(CreateTitlesList.Request request)
	{
		try
		{
			var response = await Client
				.PostAsJsonAsync(ApiRoutes.TitlesLists.Route, request);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<TitlesList>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<TitlesList>> Update(Guid id, UpdateTitlesList.Request request)
	{
		try
		{
			var response = await Client
				.PutAsJsonAsync(ApiRoutes.TitlesLists.Route + $"/{id}", request);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<TitlesList>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<DeleteResult> DeleteAsync(Guid id)
	{
		try
		{
			var response = await Client
				.DeleteAsync(ApiRoutes.TitlesLists.Route + $"/{id}");

			if (response.IsSuccessStatusCode)
				return new Success();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
