using API.Extensions;
using Application.Features.Comments;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace API.Endpoints;

public class CommentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost(ApiRoutes.Comments.CreateForTitle, CreateCommentForTitleAsync)
			.RequireAuthorization();
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<CommentResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> CreateCommentForTitleAsync(
		[FromRoute] Guid titleId,
		[FromBody] CreateCommentForTitleFromAuthor.Request request,
		ClaimsPrincipal claims,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		request.TitleId = titleId;
		request.AuthorId = claims.GetIdentifier();

		var result = await mediator.Send(request);

		return result.Match(
			comment => Results.Ok(mapper.ToResponse(comment)),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
	}
}
