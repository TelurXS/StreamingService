using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Images;

public static class CreateImageFromFile
{
	public class Request : IRequest<CreateResult<Image>>
	{
		public IFormFile File { get; set; } = default!;

		public bool AsScreenshot { get; set; } = false;
	}

	public class Handler : SyncRequestHandler<Request, CreateResult<Image>>
	{
        public Handler(IImageService imageService, IFileService fileService)
        {
			ImageService = imageService;
			FileService = fileService;
		}

		private IImageService ImageService { get; }
		private IFileService FileService { get; }

		protected override CreateResult<Image> Handle(Request request)
		{
			string url;

			if (request.AsScreenshot)
				url = FileService.UploadTitleScreenshot(request.File);
			
			else url = FileService.UploadTitleImage(request.File);

			if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var _) is false)
				return new Failed();

			var image = new Image
			{
				Uri = url,
			};

			return ImageService.Create(image);
		}
	}
}
