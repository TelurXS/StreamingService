using Application.Features.Users;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Requests;
using Domain.Models.Responses;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class IdentityService : IIdentityService
{
    public IdentityService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<CreateResult<Success>> RegisterAsync(RegisterRequest request)
	{
		try
		{
			var response = await Client.PostAsJsonAsync(ApiRoutes.Register, request);

			if (response.IsSuccessStatusCode)
				return new Success();

			var validationFailed = await response.Content
				.ReadFromJsonAsync<ValidationProblemDetails>();

			return new ValidationFailed(validationFailed!.Errors);
		}
		catch (Exception ex) 
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> ConfirmEmailAsync(string userId, string code)
	{
		try
		{
			var response = await Client
				.GetAsync($"{ApiRoutes.ConfirmEmail}?userId={userId}&code={code}");

			if (response.IsSuccessStatusCode)
				return new Success();

			return new ValidationFailed();
		}
		catch (Exception ex) 
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetAllResult<Genre>> GetFavouriteGenresAsync()
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Manage.Genres);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<List<Genre>>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> SetFavouriteGenresAsync(SetFavouriteGenresToUser.Request request)
	{
		try
		{
			var response = await Client
				.PostAsJsonAsync(ApiRoutes.Manage.Genres, request);

			if (response.IsSuccessStatusCode)
				return new Success();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<GetResult<UserResponse>> GetProfileAsync()
	{
		try
		{
			var response = await Client
				.GetAsync(ApiRoutes.Manage.Profile);

			if (response.IsSuccessStatusCode)
				return (await response.Content.ReadFromJsonAsync<UserResponse>())!;

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}

	public async Task<UpdateResult<Success>> UpdateProfileAsync(UpdateUserProfile.Request request)
	{
		try
		{
			var response = await Client.PostAsJsonAsync(ApiRoutes.Manage.Profile, request);

			if (response.IsSuccessStatusCode)
				return new Success();

			var validationFailed = await response.Content
				.ReadFromJsonAsync<ValidationFailed>();

			return validationFailed;
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
