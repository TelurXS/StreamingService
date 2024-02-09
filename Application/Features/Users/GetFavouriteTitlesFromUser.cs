using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class GetFavouriteTitlesFromUser
{
	public class Request : IRequest<GetAllResult<Title>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Title>>
	{
        public Handler(IUserService userService)
        {
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetAllResult<Title> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			return UserService.FindFavouriteTitlesById(request.UserId);
		}
	}
}
