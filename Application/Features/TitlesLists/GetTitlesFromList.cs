using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public static class GetTitlesFromList
{
	public class Request : IRequest<GetAllResult<Title>>
	{
		[JsonIgnore]
		public Guid TitlesListId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Title>>
	{
        public Handler(ITitlesListService titlesListService)
        {
			TitlesListService = titlesListService;
		}

		private ITitlesListService TitlesListService { get; }

		protected override GetAllResult<Title> Handle(Request request)
		{
			var titlesListResult = TitlesListService.FindById(request.TitlesListId);

			if (titlesListResult.IsFound is false)
				return new NotFound();

			return titlesListResult
				.AsFound
				.Titles
				.ToList();
		}
	}
}
