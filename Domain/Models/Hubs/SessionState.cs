namespace Domain.Models.Hubs;

public sealed class SessionState
{
	public Guid SeriesId { get; set; } = default;

	public float Progress { get; set; } = default;

	public bool IsPlaying { get; set; } = default;
}
