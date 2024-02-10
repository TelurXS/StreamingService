using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ViewRecord
{
	public Guid Id { get; set; }

	public required float Progress { get; set; }
	
	public required DateTime Time { get; set; }
	
	public int PercentProgress => (int)(Progress * 100);

	public User Author { get; set; } = default!;

	public Series Series { get; set; } = default!;

	public Title Title { get; set; } = default!;
}
