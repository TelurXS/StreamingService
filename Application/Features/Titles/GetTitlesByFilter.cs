using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitlesByFilter
{
	public class Request : IRequest<GetAllResult<Title>>
	{
		public TitleType? Type { get; set; } = null;

		public string? Name { get; set; } = null;

		public List<string> Genres { get; set; } = new();

		public TitleSorting Sorting { get; set; } = default;

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
			return TitleService.FilterAll(
				request.Type, 
				request.Name, 
				request.Genres, 
				request.Sorting, 
				request.Count, 
				request.Page);
		}
	}
}
