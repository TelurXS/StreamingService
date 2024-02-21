using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class CreateOrderRequest
{
	[JsonPropertyName("intent")]
	public string Intent { get; set; } = string.Empty;

	[JsonPropertyName("purchase_units")]
	public List<PurchaseUnit> PurchaseUnits { get; set; } = new();

	[JsonPropertyName("payment_source")]
	public PaymentSource PaymentSource { get; set; } = new();
}