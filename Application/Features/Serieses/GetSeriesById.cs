using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class GetSeriesById
{
	public class Request : IRequest<GetResult<Series>>
	{
		public Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetResult<Series>>
	{
		public Handler(ISeriesService seriesService)
		{
			SeriesService = seriesService;
		}

		private ISeriesService SeriesService { get; }

		protected override GetResult<Series> Handle(Request request)
		{
			return SeriesService.FindById(request.Id);
		}
	}
}
