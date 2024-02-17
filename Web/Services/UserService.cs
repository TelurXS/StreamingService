using Domain.Entities;
using Domain.Models.Results;
using Domain.Models;
using Domain.Models.Results.Unions;
using Web.Interfaces;
using System.Net.Http.Json;

namespace Web.Services;

public class UserService : IUserService
{
	public UserService(HttpClient client)
	{
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<GetAllResult<Title>> GetFavouriteTitlesAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.FavouriteTitles.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<User>> GetFollowersAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.Followers.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<User>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Genre>> GetFavouriteGenresAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.Genres.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Genre>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetResult<User>> GetProfileAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.Profile.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<User>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<User>> GetReadersAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.Readers.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<User>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<TitlesList>> GetTitlesListsAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.TitlesLists.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<TitlesList>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<ViewRecord>> GetViewRecordsAsync(Guid id)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Users.ViewRecords.Replace("{id}", id.ToString()));

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<ViewRecord>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
