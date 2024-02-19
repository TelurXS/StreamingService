using Application.Features.Titles;
using Carter;
using Domain.Interfaces.Mappings;
using Domain.Models;
using Domain.Models.Responses;
using Domain.Models.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Endpoints;

public class TitleEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost(ApiRoutes.Titles.Route, CreateTitleAsync)
			.DisableAntiforgery()
			.RequireAuthorization();

        app.MapPost(ApiRoutes.Titles.ByIdImage, UploadImageToTitleAsync)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet(ApiRoutes.Titles.BySlug, GetTitleBySlugAsync);

		app.MapGet(ApiRoutes.Titles.AllPopular, GetPopularTitlesAsync);

		app.MapGet(ApiRoutes.Titles.AllByName, GetTitlesByNameAsync);

		app.MapGet(ApiRoutes.Titles.AllByGenre, GetTitlesByGenreAsync);

		app.MapGet(ApiRoutes.Titles.AllByGenres, GetTitlesByGenresAsync);

		app.MapGet(ApiRoutes.Titles.CountAll, GetTitlesCountAsync);

		app.MapGet(ApiRoutes.Titles.CountByName, GetTitlesCountByNameAsync);

		app.MapGet(ApiRoutes.Titles.CountByGenre, GetTitlesCountByGenreAsync);

		app.MapGet(ApiRoutes.Titles.CountByGenres, GetTitlesCountByGenresAsync);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> CreateTitleAsync(
		[FromBody] CreateTitle.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper
		)
	{
		var result = await mediator.Send(request);

		return result.Match(
			title => Results.Ok(mapper.ToResponse(title)),
			invalid => Results.BadRequest(invalid.Errors),
			failed => Results.BadRequest());
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> UploadImageToTitleAsync(
		[FromRoute] Guid id,
		[FromForm] IFormFile image,
		[FromServices] IMediator mediator,
        [FromServices] IResponseMapper mapper)
    {
		var request = new SetImageToTitle.Request
		{
			File = image,
			TitleId = id
		};

        var result = await mediator.Send(request);

        return result.Match(
            success => Results.Ok(),
            notFound => Results.NotFound(),
            invalid => Results.BadRequest(),
            failed => Results.BadRequest());
    }

    [ProducesResponseType<TitleResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitleBySlugAsync(
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

	[ProducesResponseType<TitleInfoResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesByNameAsync(
		[FromQuery] string name,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetTitlesByName.Request
		{
			Name = name,
			Count = count,
			Page = page,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<TitleInfoResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesByGenreAsync(
		[FromQuery] string genre,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetTitlesByGenre.Request
		{
			Genre = genre,
			Count = count,
			Page = page,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<TitleInfoResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesByGenresAsync(
		[FromQuery] string[] genres,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetTitlesByGenres.Request
		{
			Genres = genres.ToList(),
			Count = count,
			Page = page,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<TitleInfoResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetPopularTitlesAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetPopularTitles.Request 
		{ 
			Count = count, 
			Page = page 
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesCount.Request
		{
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountByNameAsync(
		[FromQuery] string name,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesCountByName.Request
		{
			Name = name
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountByGenreAsync(
		[FromQuery] string genre,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesCountByGenre.Request
		{
			Genre = genre
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountByGenresAsync(
		[FromQuery] string[] genres,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesCountByGenres.Request
		{
			Genres = genres.ToList(),
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}
}
