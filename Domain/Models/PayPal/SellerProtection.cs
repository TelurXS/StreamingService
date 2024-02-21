using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class SellerProtection
{
	[JsonPropertyName("status")]
	public string Status { get; set; } = string.Empty;

	[JsonPropertyName("dispute_categories")]
	public List<string> DisputeCategories { get; set; } = new();
}
