using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesByGenres
{
	public class Request : IRequest<GetAllResult<Title>>
	{
		public List<string> Genres { get; set; } = new();

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
			return TitleService.FindAllByGenres(request.Genres, request.Count, request.Page);
		}
	}
}
