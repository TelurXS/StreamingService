using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesCountByType
{
	public class Request : IRequest<int>
	{
		public TitleType Type { get; set; } = default;
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
			return TitleService.CountByType(request.Type);
		}
	}
}
