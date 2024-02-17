using Domain.Models.PayPal;

namespace Domain.Interfaces.Services;

public interface IPayPalService
{
	Task<CaptureOrderResponse> CaptureOrderAsync(string orderId);
	Task<CreateOrderResponse> CreateOrderAsync(string value, string currency, string reference);
}