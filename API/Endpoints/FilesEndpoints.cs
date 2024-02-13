using Carter;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Endpoints;

public class FilesEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet(ApiRoutes.Files.UserImageByName, DownloadUserImage)
			.DisableAntiforgery();

		app.MapGet(ApiRoutes.Files.TitleImageByName, DownloadTitleImage)
			.DisableAntiforgery();

		app.MapGet(ApiRoutes.Files.TitleScreenshotByName, DownloadTitleScreenshot)
			.DisableAntiforgery();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	private IResult DownloadUserImage(
		[FromRoute] string name,
		[FromServices] IFileService fileService)
	{
		var bytes = fileService.DownloadUserImage(name);

		if (bytes is null)
			return Results.NotFound();

		var extension = Path.GetExtension(name)
			.Replace(".", "")
			.Replace("jpg", "jpeg");

		return Results.File(bytes, $"image/{extension}");
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	private IResult DownloadTitleImage(
		[FromRoute] string name,
		[FromServices] IFileService fileService)
	{
		var bytes = fileService.DownloadTitleImage(name);

		if (bytes is null)
			return Results.NotFound();

		var extension = Path.GetExtension(name)
			.Replace(".", "")
			.Replace("jpg", "jpeg");

		return Results.File(bytes, $"image/{extension}");
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	private IResult DownloadTitleScreenshot(
		[FromRoute] string name,
		[FromServices] IFileService fileService)
	{
		var bytes = fileService.DownloadTitleScreenshot(name);

		if (bytes is null)
			return Results.NotFound();

		var extension = Path.GetExtension(name)
			.Replace(".", "")
			.Replace("jpg", "jpeg");

		return Results.File(bytes, $"image/{extension}");
	}
}
