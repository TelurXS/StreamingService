using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class CaptureOrderResponse
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("status")]
	public string Status { get; set; } = string.Empty;

	[JsonPropertyName("payment_source")]
	public PaymentSource PaymentSource { get; set; } = new();

	[JsonPropertyName("purchase_units")]
	public List<PurchaseUnit> PurchaseUnits { get; set; } = new();

	[JsonPropertyName("payer")]
	public Payer Payer { get; set; } = new();

	[JsonPropertyName("links")]
	public List<Link> Links { get; set; } = new();
}
