using Application.Models;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class AddTitleToUserFavourite
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;
		
		[JsonIgnore]
		public Guid TitleId { get; set; } = default; 
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(IUserService userService, ITitleService titleService)
        {
			UserService = userService;
			TitleService = titleService;
		}

		private IUserService UserService { get; }
		private ITitleService TitleService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

			return UserService.AddTitleToFavourite(request.UserId, title);
		}
	}
}
