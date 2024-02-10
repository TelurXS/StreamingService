using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public static class AddTitleToList
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid TitlesListId { get; set; } = default;

		[JsonIgnore]
		public Guid TitleId { get; set; } = default;

		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(
			ITitlesListService titlesListService, 
			ITitleService titleService)
        {
			TitlesListService = titlesListService;
			TitleService = titleService;
		}

		private ITitlesListService TitlesListService { get; }
		private ITitleService TitleService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var titleListResult = TitlesListService.FindById(request.TitlesListId);

			if (titleListResult.IsFound is false)
				return new NotFound();

			if (titleListResult.AsFound.Author.Id != request.UserId)
				return new Failed();

			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

			return TitlesListService.AddTitleToList(request.TitlesListId, title);
		}
	}
}
