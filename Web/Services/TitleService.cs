using Domain.Entities;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class TitleService : ITitleService
{
    public TitleService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public Task<CreateResult<Title>> CreateAsync(Title value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteAsync(Title value)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteResult> DeleteByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<GetAllResult<Title>> FindAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<GetResult<Title>> FindByIdAsync(Guid id)
	{
		throw new NotImplementedException();
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

	public Task<UpdateResult<Title>> UpdateAsync(Guid id, Title value)
	{
		throw new NotImplementedException();
	}
}
