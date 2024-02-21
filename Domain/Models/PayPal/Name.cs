using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Name
{
	[JsonPropertyName("given_name")]
	public string GivenName { get; set; } = string.Empty;

	[JsonPropertyName("surname")]
	public string Surname { get; set; } = string.Empty;
}
