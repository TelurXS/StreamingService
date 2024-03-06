using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Subscriptions;

public static class GetAllSubscriptions
{
	public class Request : IRequest<GetAllResult<Subscription>>
	{
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Subscription>>
	{
		public Handler(ISubscriptionService subscriptionService)
		{
			SubscriptionService = subscriptionService;
		}

		private ISubscriptionService SubscriptionService { get; }

		protected override GetAllResult<Subscription> Handle(Request request)
		{
			return SubscriptionService.FindAll();
		}
	}
}
