using Application.Features.Serieses;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Endpoints;

public class SeriesEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Series.ById, GetSeriesByIdAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Series.All, GetAllSeriesAsync)
			.RequireAuthorization();

		app.MapGet(ApiRoutes.Series.CountAll, GetAllSeriesCountAsync)
			.RequireAuthorization();

		app.MapPost(ApiRoutes.Series.Route, CreateSeriesAsync)
			.RequireAuthorization();

		app.MapPut(ApiRoutes.Series.ById, UpdateSeriesAsync)
			.RequireAuthorization();
	}

	[ProducesResponseType<List<SeriesResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetSeriesByIdAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetSeriesById.Request
		{
			Id = id,
		};

		var result = await mediator.Send(request);

		return result.Match(
			series => Results.Ok(mapper.ToResponse(series)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType<List<SeriesResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetAllSeriesAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var request = new GetAllSeries.Request 
		{
		};

		var result = await mediator.Send(request);

		return result.Match(
			series => Results.Ok(series.Select(x => mapper.ToResponse(x))),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<int> GetAllSeriesCountAsync(
		[FromServices] IMediator mediator
		)
	{
		var request = new GetAllSeriesCount.Request
		{
		};

		var result = await mediator.Send(request);

		return result;
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<SeriesResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> CreateSeriesAsync(
		[FromBody] CreateSeries.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var result = await mediator.Send(request);

		return result.Match(
			series => Results.Ok(mapper.ToResponse(series)),
			notFound => Results.NotFound(),
			failed => Results.BadRequest()
			);
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<SeriesResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> UpdateSeriesAsync(
		[FromRoute] Guid id,
		[FromBody] UpdateSeries.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		request.Id = id;

		var result = await mediator.Send(request);

		return result.Match(
			series => Results.Ok(mapper.ToResponse(series)),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest()
			);
	}
}
