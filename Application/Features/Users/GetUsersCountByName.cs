using Application.Models;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.Features.Users;

public static class GetUsersCountByName
{
	public class Request : IRequest<int>
	{
		public string Name { get; set; } = string.Empty;
	}

	public class Handler : SyncRequestHandler<Request, int>
	{
		public Handler(IUserService userService)
		{
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override int Handle(Request request)
		{
			return UserService.CountByName(request.Name);
		}
	}
}
