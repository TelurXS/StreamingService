using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class PaymentSource
{
	[JsonPropertyName("paypal")]
	public Paypal Paypal { get; set; } = new();
}
