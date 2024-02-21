using API.Extensions;
using Application.Features.Rates;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public sealed class RateEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Rates.ByTitle, GetRateByTitleAndAuthorAsync)
			.RequireAuthorization();
	}

	[ProducesResponseType<RateResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private async Task<IResult> GetRateByTitleAndAuthorAsync(
		[FromRoute] Guid titleId,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		ClaimsPrincipal claims)
	{
		var request = new GetRateByTitleAndAuthor.Request
		{
			TitleId = titleId,
			UserId = claims.GetIdentifier(),
		};

		var result = await mediator.Send(request);

		return result.Match(
			rate => Results.Ok(mapper.ToResponse(rate)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}
}
