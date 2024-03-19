using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Serieses;

public static class AddSeriesToTitle
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid TitleId { get; set; } = default;

		[JsonIgnore]
		public Guid SeriesId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(ITitleService titleService, ISeriesService seriesService, IUnitOfWork unitOfWork)
        {
			TitleService = titleService;
			SeriesService = seriesService;
			UnitOfWork = unitOfWork;
		}

		public ITitleService TitleService { get; }
		public ISeriesService SeriesService { get; }
		public IUnitOfWork UnitOfWork { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

			var seriesResult = SeriesService.FindByIdWithTracking(request.SeriesId);

			if (seriesResult.IsFound is false)
				return new Failed();

			var series = seriesResult.AsFound;

			title.Series.Add(series);

			var result = UnitOfWork.SaveChages();

			if (result is false)
				return new Failed();

			return new Success();
		}
	}
}
