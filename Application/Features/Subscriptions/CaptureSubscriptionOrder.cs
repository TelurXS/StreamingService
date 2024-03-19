using Application.Features.Rates;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Subscriptions;

public static class CaptureSubscriptionOrder
{
    public class Request : IRequest<UpdateResult<CaptureOrderResponse>>
    {
        public string OrderId { get; set; } = string.Empty;

		[JsonIgnore]
		public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Request, UpdateResult<CaptureOrderResponse>>
    {
        private static readonly List<Subscription> _subscriptions = [
            Subscription.Standart,
            Subscription.Premium,
        ];

        public Handler(
            IPayPalService payPalService,
            ISubscriptionService subscriptionService,
            IUserService userService)
        {
            PayPalService = payPalService;
            SubscriptionService = subscriptionService;
            UserService = userService;
        }

        private IPayPalService PayPalService { get; }
        private ISubscriptionService SubscriptionService { get; }
        private IUserService UserService { get; }

        public async Task<UpdateResult<CaptureOrderResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await PayPalService.CaptureOrderAsync(request.OrderId);

            var reference = response.PurchaseUnits.FirstOrDefault()?.ReferenceId;

            if (reference is null)
                return new Failed();

            var subscriptionName = _subscriptions.FirstOrDefault(x => x.Name == reference)?.Name;

            if (subscriptionName is null)
                return new Failed();

            var subscriptionResult = SubscriptionService.FindByNameWithTracking(subscriptionName);

            if (subscriptionResult.IsFound is false)
                return new Failed();

            var userResult = UserService.FindById(request.UserId);

            if (userResult.IsFound is false)
                return new Failed();

            var subscription = subscriptionResult.AsFound;
            var expiresIn = DateTime.Now.AddDays(Subscription.ACTIVE_DAYS);

            var result = UserService.SetSubscription(request.UserId, subscription, expiresIn);

            if (result.IsUpdated)
                return response;

            return new Failed();
        }
    }
}
