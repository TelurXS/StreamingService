using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Infrastructure.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Serieses;

public static class DeleteSeriesById
{
    public class Request : IRequest<DeleteResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
    }

    public class Handler : SyncRequestHandler<Request, DeleteResult>
    {
        public Handler(ISeriesService seriesService, IFileService fileService)
        {
            SeriesService = seriesService;
			FileService = fileService;
		}

        private ISeriesService SeriesService { get; }
		private IFileService FileService { get; }

		protected override DeleteResult Handle(Request request)
        {
            var seriesResult = SeriesService.FindById(request.Id);

            if (seriesResult.IsFound is false)
                return new NotFound();

            var series = seriesResult.AsFound;

			var currentImage = series.Uri.Split("/").LastOrDefault();

			if (currentImage is not null)
				FileService.DeleteSeries(currentImage);

			return SeriesService.DeleteById(request.Id);
        }
    }
}
