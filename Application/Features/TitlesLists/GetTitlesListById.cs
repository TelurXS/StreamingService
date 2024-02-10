using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public static class GetTitlesListById
{
	public class Request : IRequest<GetResult<TitlesList>>
	{
		[JsonIgnore]
		public Guid Id { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetResult<TitlesList>>
	{
        public Handler(ITitlesListService titlesListService)
        {
			TitlesListService = titlesListService;
		}

		private ITitlesListService TitlesListService { get; }

		protected override GetResult<TitlesList> Handle(Request request)
		{
			return TitlesListService.FindById(request.Id);
		}
	}
}
