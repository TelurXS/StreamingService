using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Payments
{
	[JsonPropertyName("captures")]
	public List<Capture> Captures { get; set; } = new();
}
