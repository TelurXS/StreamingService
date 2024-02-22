namespace Domain.Models.Hubs;

public class Session
{
	public Guid Id { get; set; } = default;

	public Guid TitleId { get; set; } = default;

	public List<string> Connections { get; set; } = new();

	public string Host { get; set; } = string.Empty;
}
