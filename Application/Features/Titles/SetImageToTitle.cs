using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.Features.Titles;

public static class SetImageToTitle
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		public IFormFile File { get; set; } = default!;

		[JsonIgnore]
		public Guid TitleId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(ITitleService titleService, IFileService fileService)
        {
			TitleService = titleService;
			FileService = fileService;
		}

		private ITitleService TitleService { get; }
		private IFileService FileService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var titleResult = TitleService.FindById(request.TitleId);
			
			if (titleResult.IsFound is false)
				return new NotFound();
			
			var title = titleResult.AsFound;
			
			var url = FileService.UploadTitleImage(request.File);
			
			if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var _) is false)
				return new Failed();

			var image = new Image { Uri = url };

			return TitleService.SetImage(title.Id, image);
		}
	}
}
