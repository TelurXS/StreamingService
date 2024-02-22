using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class GetTitleById
{
	public class Request : IRequest<GetResult<Title>>
	{
		public Guid Id { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetResult<Title>>
	{
		public Handler(ITitleService titleService)
		{
			TitleService = titleService;
		}

		private ITitleService TitleService { get; }

		protected override GetResult<Title> Handle(Request request)
		{
			return TitleService.FindById(request.Id);
		}
	}
}
