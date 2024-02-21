using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Shipping
{
	[JsonPropertyName("address")]
	public Address Address { get; set; } = new();
}
