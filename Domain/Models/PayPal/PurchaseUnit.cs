using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class PurchaseUnit
{
	[JsonPropertyName("amount")]
	public Amount Amount { get; set; } = new();

	[JsonPropertyName("reference_id")]
	public string ReferenceId { get; set; } = string.Empty;

	[JsonPropertyName("shipping")]
	public Shipping? Shipping { get; set; } = new();

	[JsonPropertyName("payments")]
	public Payments? Payments { get; set; } = new();
}
