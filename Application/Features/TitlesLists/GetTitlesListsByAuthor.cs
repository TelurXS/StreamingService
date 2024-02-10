using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public class GetTitlesListsByAuthor
{
	public class Request : IRequest<GetAllResult<TitlesList>>
	{
		[JsonIgnore]
		public Guid AuthorId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<TitlesList>>
	{
		public Handler(ITitlesListService titlesListService, IUserService userService)
		{
			TitlesListService = titlesListService;
			UserService = userService;
		}

		private ITitlesListService TitlesListService { get; }
		private IUserService UserService { get; }

		protected override GetAllResult<TitlesList> Handle(Request request)
		{
			var authorResult = UserService.FindById(request.AuthorId);

			if (authorResult.IsFound is false)
				return new NotFound();

			return TitlesListService.FindAllByAuthor(authorResult.AsFound);
		}
	}
}
