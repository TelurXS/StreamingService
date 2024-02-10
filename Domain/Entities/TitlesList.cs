using Domain.Models;

namespace Domain.Entities;

public class TitlesList
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required Availability Availability { get; set; }

	public User Author { get; set; } = default!;

	public ICollection<Title> Titles { get; set; } = default!;
}
