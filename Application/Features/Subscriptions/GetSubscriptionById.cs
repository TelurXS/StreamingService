using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Subscriptions;

public static class GetSubscriptionById
{
	public class Request : IRequest<GetResult<Subscription>>
	{
		public Guid Id { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetResult<Subscription>>
	{
		public Handler(ISubscriptionService subscriptionService)
		{
			SubscriptionService = subscriptionService;
		}

		private ISubscriptionService SubscriptionService { get; }

		protected override GetResult<Subscription> Handle(Request request)
		{
			return SubscriptionService.FindById(request.Id);
		}
	}
}
