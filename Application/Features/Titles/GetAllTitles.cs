using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class GetAllTitles
{
	public class Request : IRequest<GetAllResult<Title>>
	{
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
			return TitleService.FindAll(request.Count, request.Page);
		}
	}
}
