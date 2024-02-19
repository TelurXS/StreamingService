using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Application.Features.Subscriptions;

public static class CreateSubscriptionOrder
{
    public class Request : IRequest<CreateResult<CreateOrderResponse>> 
    {
        public string SubscriptionName { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Request, CreateResult<CreateOrderResponse>>
    {
        private static readonly List<Subscription> _subscriptions = [
            Subscription.Standart,
            Subscription.Premium,
        ];

        public Handler(IPayPalService payPalService)
        {
            PayPalService = payPalService;
        }

        private IPayPalService PayPalService { get; }

        public async Task<CreateResult<CreateOrderResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            var subscription = _subscriptions.FirstOrDefault(x => x.Name == request.SubscriptionName);

            if (subscription is null)
                return new Failed();

            var price = subscription.Price.ToString().Replace(",", ".");
            var reference = request.SubscriptionName;

            return await PayPalService.CreateOrderAsync(price, Subscription.CURRENCY, reference);
        }
    }
}
