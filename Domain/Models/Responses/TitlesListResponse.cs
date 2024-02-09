using Domain.Entities;

namespace Domain.Models.Responses;

public class TitlesListResponse
{
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public Availability Availability { get; set; }

	public ICollection<TitleInfoResponse> Titles { get; set; } = default!;
}
