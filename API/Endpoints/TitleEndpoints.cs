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

        app.MapPost(ApiRoutes.Titles.ImageById, UploadImageToTitleAsync)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet(ApiRoutes.Titles.ById, GetTitleByIdAsync);

        app.MapGet(ApiRoutes.Titles.BySlug, GetTitleBySlugAsync);

        app.MapGet(ApiRoutes.Titles.All, GetTitlesAsync);

		app.MapGet(ApiRoutes.Titles.AllPopular, GetPopularTitlesAsync);

		app.MapGet(ApiRoutes.Titles.AllByName, GetTitlesByNameAsync);

		app.MapGet(ApiRoutes.Titles.AllByType, GetTitlesByTypeAsync);

		app.MapGet(ApiRoutes.Titles.AllByGenre, GetTitlesByGenreAsync);

		app.MapGet(ApiRoutes.Titles.AllByGenres, GetTitlesByGenresAsync);

		app.MapGet(ApiRoutes.Titles.AllByFilter, GetTitlesByFilterAsync);

		app.MapGet(ApiRoutes.Titles.CountAll, GetTitlesCountAsync);

		app.MapGet(ApiRoutes.Titles.CountByName, GetTitlesCountByNameAsync);

		app.MapGet(ApiRoutes.Titles.CountByType, GetTitlesCountByTypeAsync);

		app.MapGet(ApiRoutes.Titles.CountByGenre, GetTitlesCountByGenreAsync);

		app.MapGet(ApiRoutes.Titles.CountByGenres, GetTitlesCountByGenresAsync);

		app.MapGet(ApiRoutes.Titles.CountByFilter, GetTitlesCountByFilterAsync);

		app.MapPut(ApiRoutes.Titles.ById, UpdateTitleByIdAsync);
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
	private static async Task<IResult> GetTitleByIdAsync(
		[FromRoute] Guid id,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitleById.Request
		{
			Id = id
		};

		var result = await mediator.Send(request);

		return result.Match(
			title => Results.Ok(mapper.ToResponse(title)),
			NotFound => Results.NotFound(),
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

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetAllTitles.Request
		{
			Count = count,
			Page = page,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesByTypeAsync(
		[FromQuery] TitleType type,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetTitlesByType.Request
		{
			Type = type,
			Count = count,
			Page = page,
		};

		var result = await mediator.Send(request);

		return result.Match(
			titles => Results.Ok(titles.Select(x => mapper.ToInfoResponse(x))),
			notFound => Results.BadRequest(),
			failed => Results.BadRequest());
	}

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
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

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
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

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
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

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
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

	[ProducesResponseType<List<TitleInfoResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesByFilterAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] string[] genres,
		[FromQuery] TitleType? type = null,
		[FromQuery] string? name = null,
		[FromQuery] TitleSorting sorting = default,
		[FromQuery] int count = 10,
		[FromQuery] int page = 0
		)
	{
		var request = new GetTitlesByFilter.Request
		{
			Type = type,
			Name = name,
			Genres = genres.ToList(),
			Sorting = sorting,
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
		var request = new GetAllTitlesCount.Request
		{
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountByTypeAsync(
		[FromQuery] TitleType type,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		var request = new GetTitlesCountByType.Request
		{
			Type = type,
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

	[ProducesResponseType<int>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> GetTitlesCountByFilterAsync(
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper,
		[FromQuery] string[] genres,
		[FromQuery] TitleType? type = null,
		[FromQuery] string? name = null,
		[FromQuery] TitleSorting sorting = default
		)
	{
		var request = new GetTitlesCountByFilter.Request
		{
			Type = type,
			Name = name,
			Genres = genres.ToList(),
			Sorting = sorting,
		};

		var result = await mediator.Send(request);

		return Results.Ok(result);
	}

	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType<TitleResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	private static async Task<IResult> UpdateTitleByIdAsync(
		[FromRoute] Guid id,
		[FromBody] UpdateTitle.Request request,
		[FromServices] IMediator mediator,
		[FromServices] IResponseMapper mapper)
	{
		request.Id = id;

		var result = await mediator.Send(request);

		return result.Match(
			title => Results.Ok(mapper.ToResponse(title)),
			notFound => Results.NotFound(),
			invalid => Results.BadRequest(),
			failed => Results.BadRequest());
	}
}
