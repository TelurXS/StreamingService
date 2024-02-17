using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Paypal
{
	[JsonPropertyName("name")]
	public Name? Name { get; set; } = new();

	[JsonPropertyName("email_address")]
	public string? EmailAddress { get; set; } = string.Empty;

	[JsonPropertyName("account_id")]
	public string? AccountId { get; set; } = string.Empty;

	[JsonPropertyName("experience_context")]
	public ExperienceContext? ExperienceContext { get; set; } = new();
}
