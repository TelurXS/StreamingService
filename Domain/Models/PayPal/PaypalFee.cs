using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class PaypalFee
{
	[JsonPropertyName("currency_code")]
	public string CurrencyCode { get; set; } = string.Empty;

	[JsonPropertyName("value")]
	public string Value { get; set; } = string.Empty;
}
