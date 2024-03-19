using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using Domain.Models.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.Features.Titles;

public static class SetScreenshotsToTitle
{
    public class Request : IRequest<UpdateResult<Success>>
    {
        public List<IFormFile> Files { get; set; } = [];

        [JsonIgnore]
        public Guid TitleId { get; set; } = default;
    }

    public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
    {
        public Handler(ITitleService titleService, IFileService fileService, IUnitOfWork unitOfWork)
        {
            TitleService = titleService;
            FileService = fileService;
            UnitOfWork = unitOfWork;
        }

        private ITitleService TitleService { get; }
        private IFileService FileService { get; }
        private IUnitOfWork UnitOfWork { get; }

        protected override UpdateResult<Success> Handle(Request request)
        {
            var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

            if (titleResult.IsFound is false)
                return new NotFound();

            var title = titleResult.AsFound;

            foreach (var screenshot in title.Screenshots)
            {
                var screenshotUrl = screenshot.Uri.Split("/").LastOrDefault();

                if (screenshotUrl is not null)
                    FileService.DeleteTitleScreenshot(screenshotUrl);
            }

            title.Screenshots.Clear();

            foreach (var file in request.Files)
            {
                var url = FileService.UploadTitleScreenshot(file);

                if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var _) is false)
                    return new Failed();

                var image = new Image { Uri = url };

                title.Screenshots.Add(image);
            }

            if (UnitOfWork.SaveChages())
                return new Success();

            return new Failed();
        }
    }
}
