using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Payer
{
	[JsonPropertyName("name")]
	public Name Name { get; set; } = new();

	[JsonPropertyName("email_address")]
	public string EmailAddress { get; set; } = string.Empty;

	[JsonPropertyName("payer_id")]
	public string PayerId { get; set; } = string.Empty;
}
