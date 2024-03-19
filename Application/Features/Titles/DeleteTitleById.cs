using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Titles;

public static class DeleteTitleById
{
    public class Request : IRequest<DeleteResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
    }

    public class Handler : SyncRequestHandler<Request, DeleteResult>
    {
        public Handler(ITitleService titleService, IFileService fileService)
        {
            TitleService = titleService;
			FileService = fileService;
		}

        private ITitleService TitleService { get; }
		private IFileService FileService { get; }

		protected override DeleteResult Handle(Request request)
        {
            var titleResult = TitleService.FindByIdWithTracking(request.Id);

            if (titleResult.IsFound is false)
                return new NotFound();

            var title = titleResult.AsFound;

			var currentImage = title.Image?.Uri.Split("/").LastOrDefault();

			if (currentImage is not null)
				FileService.DeleteTitleImage(currentImage);

			foreach (var screenshot in title.Screenshots)
			{
				var screenshotUrl = screenshot.Uri.Split("/").LastOrDefault();

				if (screenshotUrl is not null)
					FileService.DeleteTitleScreenshot(screenshotUrl);
			}

			return TitleService.Delete(title);
        }
    }
}
