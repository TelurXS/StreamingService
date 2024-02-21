using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class AddUserToFollowers
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		public Guid UserId { get; set; }

		public Guid FollowerId { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(IUserService userService)
        {
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindByIdWithTracking(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			var followerResult = UserService.FindByIdWithTracking(request.FollowerId);

			if (followerResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;
			var follower = followerResult.AsFound;

			return UserService.AddUserToFollowers(follower, user);
		}
	}
}
