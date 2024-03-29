﻿using System.Text.Json.Serialization;

namespace Domain.Models.PayPal;

public sealed class Link
{
	[JsonPropertyName("href")]
	public string Href { get; set; } = string.Empty;

	[JsonPropertyName("rel")]
	public string Rel { get; set; } = string.Empty;

	[JsonPropertyName("method")]
	public string Method { get; set; } = string.Empty;
}
