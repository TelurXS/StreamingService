using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesByLanguage
{
	public class Request : IRequest<GetAllResult<Title>>
	{
		public string Language { get; set; } = string.Empty;

		public TitleSorting Sorting { get; set; } = TitleSorting.None;

		public int Count { get; set; } = 10;

		public int Page { get; set; } = 0;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Title>>
	{
		public Handler(ITitleService titleService)
		{
			TitleService = titleService;
		}

		private ITitleService TitleService { get; }

		protected override GetAllResult<Title> Handle(Request request)
		{
			return TitleService.FindAllByLanguage(request.Language, request.Sorting, request.Count, request.Page);
		}
	}
}
