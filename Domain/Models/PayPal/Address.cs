using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public class Address
{
	[JsonPropertyName("address_line_1")]
	public string AddressLine1 { get; set; } = string.Empty;

	[JsonPropertyName("address_line_2")]
	public string AddressLine2 { get; set; } = string.Empty;

	[JsonPropertyName("admin_area_2")]
	public string AdminArea2 { get; set; } = string.Empty;

	[JsonPropertyName("admin_area_1")]
	public string AdminArea1 { get; set; } = string.Empty;

	[JsonPropertyName("postal_code")]
	public string PostalCode { get; set; } = string.Empty;

	[JsonPropertyName("country_code")]
	public string CountryCode { get; set; } = string.Empty;
}
