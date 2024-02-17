namespace Domain.Models.Responses;

public class SeriesResponse
{
	public Guid Id { get; set; } = default;

	public string Name { get; set; } = string.Empty;

	public string Uri { get; set; } = string.Empty;
	
	public string Dubbing { get; set; } = string.Empty;

	public string Language { get; set; } = string.Empty;

	public int Index { get; set; } = default;
}
