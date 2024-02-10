using Domain.Entities;

namespace Domain.Models.Responses;

public class ViewRecordResponse
{
	public Guid Id { get; set; }

	public float Progress { get; set; }
	
	public DateTime Time { get; set; }

	public SeriesResponse Series { get; set; } = default!;

	public TitleInfoResponse Title { get; set; } = default!;
}
