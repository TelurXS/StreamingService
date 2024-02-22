using Domain.Entities;
using Domain.Models.Results;
using Domain.Models;
using Domain.Models.Results.Unions;
using Web.Interfaces;
using System.Net.Http.Json;
using Application.Features.Comments;

namespace Web.Services;

public class CommentService : ICommentsService
{
	public CommentService(HttpClient client)
	{
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<CreateResult<Comment>> CreateCommentForTitle(Guid id, CreateCommentForTitleFromAuthor.Request request)
	{
		try
		{
			var response = await Client
				.PostAsJsonAsync(ApiRoutes.Comments.Route + $"/{id}", request);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<Comment>();

			return new Failed();
		}
		catch (Exception ex)
		{
			return new Failed(ex.Message);
		}
	}
}
