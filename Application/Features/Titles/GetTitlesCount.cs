using Application.Models;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesCount
{
	public class Request : IRequest<int>
	{
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
			return TitleService.Count();
		}
	}
}
