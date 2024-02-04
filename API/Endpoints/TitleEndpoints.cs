using Application.Features.Titles;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class TitleEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Titles.BySlug, GetTitleBySlugAsync);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetTitleBySlugAsync(
		[FromRoute] string slug,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitleBySlug.Request 
		{ 
			Slug = slug 
		};

		var result = await mediator.Send(request);

		return result.Match(
			title => Results.Ok(mapper.ToResponse(title)),
			NotFound => Results.NotFound(),
			failed => Results.BadRequest());
	}
}
