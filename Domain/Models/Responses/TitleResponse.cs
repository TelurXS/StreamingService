namespace Domain.Models.Responses;

public class TitleResponse
{
	public Guid Id { get; set; } = default;

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string Slug { get; set; } = string.Empty;

	public float AvarageRate { get; set; } = default;

	public DateTime ReleaseDate { get; set; } = default;

	public Country Country { get; set; } = default;

	public AgeRestriction AgeRestriction { get; set; } = default;

	public string Director { get; set; } = string.Empty;

	public string Cast { get; set; } = string.Empty;

	public int Views { get; set; } = default;

	public ICollection<NameResponse> Names { get; set; } = default!;

	public ICollection<DescriptionResponse> Descriptions { get; set; } = default!;

	public ImageResponse Image { get; set; } = default!;

	public ICollection<ImageResponse> Screenshots { get; set; } = default!;

	public ICollection<GenreResponse> Genres { get; set; } = default!;

	public ICollection<SeriesResponse> Series { get; set; } = default!;

	public ICollection<CommentResponse> Comments { get; set; } = default!;
}
