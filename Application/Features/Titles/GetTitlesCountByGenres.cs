using Application.Models;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesCountByGenres
{
	public class Request : IRequest<int>
	{
		public List<string> Genres { get; set; } = new();
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
			return TitleService.CountByGenres(request.Genres);
		}
	}
}
