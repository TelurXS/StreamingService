using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Subscriptions;

public static class ApplyTrialSubscriptionToUser
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		public Guid UserId { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(
			IUserService userService, 
			ISubscriptionService subscriptionService)
        {
			UserService = userService;
			SubscriptionService = subscriptionService;
		}

		private IUserService UserService { get; }
		private ISubscriptionService SubscriptionService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;

			if (user.IsTrialSubscriptionUsed)
				return new Failed();

			var subscriptionResult = SubscriptionService
				.FindByNameWithTracking(Subscription.Trial.Name);

			if (subscriptionResult.IsFound is false)
				return new Failed();

			var subscription = subscriptionResult.AsFound;
			var expireAt = DateTime.Now.AddDays(Subscription.TRIAL_ACTIVE_DAYS);

			user.IsTrialSubscriptionUsed = true;

			if (UserService.Update(request.UserId, user).IsUpdated is false)
				return new Failed();

			return UserService.SetSubscription(request.UserId, subscription, expireAt);
		}
	}
}
