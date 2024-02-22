using Application.Features.Titles;
using Azure;
using Domain.Entities;
using Domain.Models;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class TitleService : ITitleService
{
    public TitleService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<GetResult<Title>> FindByIdAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.All + $"/{id}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Title>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Title>> FindAllAsync(int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.All + $"?count={count}&page={page}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		};
	}

	public async Task<GetAllResult<Title>> FindAllPopularAsync(int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllPopular + $"?count={count}&page={page}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Title>> FindAllByNameAsync(string name, int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByName + $"?name={name}&count={count}&page={page}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Title>> FindAllByGenreAsync(string genre, int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByGenre + $"?genre={genre}&count={count}&page={page}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Title>> FindAllByGenresAsync(List<string> genres, int count = 10, int page = 0)
	{
		try
		{
			var query = $"?count={count}&page={page}" + string.Concat(genres.Select(x => "&genres=" + x));

			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByGenres + query);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetResult<Title>> FindBySlugAsync(string slug)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.Route + $"/{slug}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Title>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<CreateResult<Title>> CreateAsync(CreateTitle.Request value)
	{
		try
		{
			var response = await Client
				.PostAsJsonAsync(ApiRoutes.Titles.All, value);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Title>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Title>> UpdateAsync(Guid id, UpdateTitle.Request value)
	{
		try
		{
			var response = await Client
				.PutAsJsonAsync(ApiRoutes.Titles.All + $"/{id}", value);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Title>();

			return new NotFound();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public Task<DeleteResult> DeleteByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteAsync(Title value)
	{
		throw new NotImplementedException();
	}

	public async Task<int> CountAsync()
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountAll);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public async Task<int> CountByNameAsync(string name)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountByName + $"?name={name}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public async Task<int> CountByGenreAsync(string genre)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountByGenre + $"?genre={genre}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public async Task<int> CountByGenresAsync(List<string> genres)
	{
		try
		{
			var query = "?" + string.Concat(genres.Select(x => "genres=" + x + "&"));

			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByGenres + query);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}
}
