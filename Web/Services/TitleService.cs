﻿using Application.Features.Titles;
using Azure;
using Domain.Entities;
using Domain.Models;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components.Forms;
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

	public async Task<GetAllResult<Title>> FindAllByTypeAsync(TitleType type, int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByType + $"?type={(int)type}&count={count}&page={page}");

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

	public async Task<GetAllResult<Title>> FindAllByLanguageAsync(string language, TitleSorting sorting = TitleSorting.None, int count = 10, int page = 0)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByLanguage + $"?language={language}&sorting={(int)sorting}&count={count}&page={page}");

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

	public async Task<GetAllResult<Title>> FilterAllAsync(List<string> genres, TitleType? type = null, string? name = null, TitleSorting sorting = TitleSorting.None, int count = 10, int page = 0)
	{
		try
		{
			var query = $"?count={count}&page={page}&";

			if (genres.Any())
				query += string.Concat(genres.Select(x => "genres=" + x + "&"));

			if (type is not null)
				query += $"type={(int?)type}&";

			if (name is not null)
				query += $"name={name}&";

			if (sorting is not TitleSorting.None)
				query += $"sorting={(int)sorting}";

			var response = await Client
				.GetAsync(ApiRoutes.Titles.AllByFilter + query);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Title>>();

			return new Failed();
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

			return new ValidationFailed(
				await response.Content.ReadFromJsonAsync<List<ValidationFailure>>()
				);
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

			return await response.Content.ReadFromJsonAsync<ValidationFailed>();
        }
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<DeleteResult> DeleteByIdAsync(Guid id)
	{
        try
        {
            var response = await Client
                .DeleteAsync(ApiRoutes.Titles.Route + $"/{id}");

            if (response.IsSuccessStatusCode)
                return new Success();

            return new NotFound();
        }
        catch (Exception ex)
        {
            return new Failed(ex.Message);
        }
    }

	public Task<DeleteResult> DeleteAsync(Title value)
	{
		return DeleteByIdAsync(value.Id);
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

	public async Task<int> CountByTypeAsync(TitleType type)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountByType + $"?type={(int)type}");

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

	public async Task<int> CountByFilter(List<string> genres, TitleType? type = null, string? name = null, TitleSorting sorting = TitleSorting.None)
	{
		try
		{
			var query = "?";

			if (genres.Any())
				query += string.Concat(genres.Select(x => "genres=" + x + "&"));

			if (type is not null)
				query += $"type={(int?)type}&";

			if (name is not null)
				query += $"name={name}&";

			if (sorting is not TitleSorting.None)
				query += $"sorting={(int)sorting}";

			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountByFilter + query);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public async Task<int> CountByLanguageAsync(string language)
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Titles.CountByLanguage + $"?language={language}");

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<int>();

			return default;
		}
		catch
		{
			return default;
		}
	}

	public async Task<UpdateResult<Success>> UploadImageAsync(Guid id, IBrowserFile file)
	{
		try
		{
			var data = new MultipartFormDataContent
			{
				{ new StreamContent(file.OpenReadStream(5120000)), "image", file.Name }
			};

			var response = await Client
				.PostAsync(ApiRoutes.Titles.ByIdImage.Replace("{id:guid}", id.ToString()), data);

			if (response.IsSuccessStatusCode)
				return new Success();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> UploadScreenshotAsync(Guid id, List<IBrowserFile> files)
	{
		try
		{
			var data = new MultipartFormDataContent();

			foreach (var file in files)
			{
				data.Add(new StreamContent(file.OpenReadStream(5120000)), "screenshots", file.Name);
			}

			var response = await Client
				.PostAsync(ApiRoutes.Titles.ByIdScreenshots.Replace("{id:guid}", id.ToString()), data);

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
