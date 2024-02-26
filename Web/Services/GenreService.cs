using Application.Features.Genres;
using Domain.Entities;
using Domain.Models.Results;
using Domain.Models;
using Domain.Models.Results.Unions;
using Web.Interfaces;
using System.Net.Http.Json;

namespace Web.Services;

public class GenreService : IGenreService
{
    public GenreService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public Task<int> CountAsync()
	{
		throw new NotImplementedException();
	}

	public Task<CreateResult<Genre>> CreateAsync(CreateGenre.Request value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteAsync(Genre value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<GetAllResult<Genre>> FindAllAsync(int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Genres.All);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Genre>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public Task<GetResult<Genre>> FindByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<UpdateResult<Genre>> UpdateAsync(Guid id, UpdateGenre.Request value)
	{
		throw new NotImplementedException();
	}
}
