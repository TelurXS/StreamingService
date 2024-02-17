using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class GetReadersFromUser
{
	public class Request : IRequest<GetAllResult<User>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<User>>
	{
        public Handler(IUserService userService)
        {
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetAllResult<User> Handle(Request request)
		{
			var userResult = UserService.FindById(request.Id);

			if (userResult.IsFound is false)
				return new Failed();

			return UserService.FindReadersFromUser(request.Id);
		}
	}
}
