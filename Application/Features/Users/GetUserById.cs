using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class GetUserById
{
	public class Request : IRequest<GetResult<User>>
	{
		public required Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetResult<User>>
	{
        public Handler(IUserService userService)
        {
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetResult<User> Handle(Request request)
		{
			return UserService.FindById(request.Id);
		}
	}
}
