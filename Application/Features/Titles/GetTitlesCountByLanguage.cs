using Application.Models;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesCountByLanguage
{
	public class Request : IRequest<int>
	{
		public string Language { get; set; } = string.Empty;
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
			return TitleService.CountByLanguage(request.Language);
		}
	}
}
