using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public class Capture
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("status")]
	public string Status { get; set; } = string.Empty;

	[JsonPropertyName("amount")]
	public Amount Amount { get; set; } = new();

	[JsonPropertyName("seller_protection")]
	public SellerProtection SellerProtection { get; set; } = new();

	[JsonPropertyName("final_capture")]
	public bool FinalCapture { get; set; } = default;

	[JsonPropertyName("disbursement_mode")]
	public string DisbursementMode { get; set; } = string.Empty;

	[JsonPropertyName("seller_receivable_breakdown")]
	public SellerReceivableBreakdown SellerReceivableBreakdown { get; set; } = new();

	[JsonPropertyName("create_time")]
	public DateTime CreateTime { get; set; } = default;

	[JsonPropertyName("update_time")]
	public DateTime UpdateTime { get; set; } = default;

	[JsonPropertyName("links")]
	public List<Link> Links { get; set; } = new();
}
