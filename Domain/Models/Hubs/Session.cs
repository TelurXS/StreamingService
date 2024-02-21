namespace Domain.Models.Hubs;

public class Session
{
	public Guid Id { get; set; } = default;

	public Guid TitleId { get; set; } = default;

	public SessionState State { get; set; } = new();

	public List<string> Connections { get; set; } = new();
}
