using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ViewRecord
{
	public Guid Id { get; set; }

	public required float Progress { get; set; }

	public User Author { get; set; } = default!;

	public Series Series { get; set; } = default!;

	public Title Title { get; set; } = default!;
}
