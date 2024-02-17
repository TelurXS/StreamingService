using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class ExperienceContext
{
	[JsonPropertyName("shipping_preference")]
	public string ShippingPreference { get; set; } = string.Empty;
}
