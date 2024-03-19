using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.Features.Serieses;

public static class SetSeriesUriFromFile
{
	public class Request : IRequest<UpdateResult<Series>>
	{
		[JsonIgnore]
		public Guid Id { get; set; } = default;

		public IFormFile File { get; set; } = default!;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Series>>
	{
        public Handler(ISeriesService seriesService, IFileService fileService)
        {
			SeriesService = seriesService;
			FileService = fileService;
		}

		private ISeriesService SeriesService { get; }
		private IFileService FileService { get; }

		protected override UpdateResult<Series> Handle(Request request)
		{
			var seriesResult = SeriesService.FindById(request.Id);

			if (seriesResult.IsFound is false)
				return new NotFound();

			var series = seriesResult.AsFound;

			var currentImage = series.Uri.Split("/").LastOrDefault();

			if (currentImage is not null)
				FileService.DeleteSeries(currentImage);

			var url = FileService.UploadSeries(request.File);

			if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var _) is false)
				return new Failed();

			series.Uri = url;

			return SeriesService.Update(request.Id, series);
		}
	}
}
