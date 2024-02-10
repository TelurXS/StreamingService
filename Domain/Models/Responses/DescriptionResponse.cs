namespace Domain.Models.Responses;

public class DescriptionResponse
{
	public Guid Id { get; set; } = default;

	public string Language { get; set; } = string.Empty;

	public string Value { get; set; } = string.Empty;
}
