using Application.Models;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.Features.Serieses;

public static class GetAllSeriesCount
{
	public class Request : IRequest<int>
	{
	}

	public class Handler : SyncRequestHandler<Request, int>
	{
		public Handler(ISeriesService seriesService)
		{
			SeriesService = seriesService;
		}

		private ISeriesService SeriesService { get; }

		protected override int Handle(Request request)
		{
			return SeriesService.Count();
		}
	}
}
