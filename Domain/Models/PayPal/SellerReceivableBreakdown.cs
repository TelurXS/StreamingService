using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class SellerReceivableBreakdown
{
	[JsonPropertyName("gross_amount")]
	public Amount GrossAmount { get; set; } = new();

	[JsonPropertyName("paypal_fee")]
	public PaypalFee PaypalFee { get; set; } = new();

	[JsonPropertyName("net_amount")]
	public Amount NetAmount { get; set; } = new();
}
