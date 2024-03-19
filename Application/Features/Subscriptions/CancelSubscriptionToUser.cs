using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Infrastructure.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Subscriptions;

public static class CancelSubscriptionToUser
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
		public Handler(
			IUserService userService)
		{
			UserService = userService;
		}

		private IUserService UserService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			if (UserService.FindById(request.UserId).IsFound is false)
				return new Failed();

			return UserService.SetSubscription(request.UserId, null, DateTime.Now);
		}
	}
}
