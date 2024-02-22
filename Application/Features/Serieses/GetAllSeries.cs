using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class GetAllSeries
{
	public class Request : IRequest<GetAllResult<Series>>
	{
		public int Count { get; set; } = 10;

		public int Page { get; set; } = 0;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Series>>
	{
		public Handler(ISeriesService seriesService)
		{
			SeriesService = seriesService;
		}

		private ISeriesService SeriesService { get; }

		protected override GetAllResult<Series> Handle(Request request)
		{
			return SeriesService.FindAll(request.Count, request.Page);
		}
	}
}
