using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesCountByFilter
{
	public class Request : IRequest<int>
	{
		public TitleType? Type { get; set; } = null;

		public string? Name { get; set; } = null;

		public List<string> Genres { get; set; } = new();

		public TitleSorting Sorting { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, int>
	{
		public Handler(ITitleService titleService)
		{
			TitleService = titleService;
		}

		private ITitleService TitleService { get; }

		protected override int Handle(Request request)
		{
			return TitleService.CountByFilter(
				request.Type,
				request.Name,
				request.Genres,
				request.Sorting);
		}
	}
}
