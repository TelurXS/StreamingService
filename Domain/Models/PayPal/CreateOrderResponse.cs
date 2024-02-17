using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class CreateOrderResponse
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("status")]
	public string Status { get; set; } = string.Empty;

	[JsonPropertyName("links")]
	public List<Link> Links { get; set; } = new();
}
