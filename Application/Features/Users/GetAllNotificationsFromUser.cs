using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using Domain.Models.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class GetAllNotificationsFromUser
{
	public class Request : IRequest<GetAllResult<Notification>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Notification>>
	{
		public Handler(IUserService userService)
		{
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override GetAllResult<Notification> Handle(Request request)
		{
			var userResult = UserService.FindById(request.Id);

			if (userResult.IsFound is false)
				return new Failed();

			return UserService.FindNotificationsFromUser(request.Id);
		}
	}
}
