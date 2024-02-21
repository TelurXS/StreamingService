namespace Domain.Models.Responses;

public class TitleInfoResponse
{
	public Guid Id { get; set; } = default;

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string Slug { get; set; } = string.Empty;

	public float AvarageRate { get; set; } = default;

	public DateTime ReleaseDate { get; set; } = default;

    public TitleType Type { get; set; } = default;

    public Country Country { get; set; } = default;

	public AgeRestriction AgeRestriction { get; set; } = default;

	public string Director { get; set; } = string.Empty;

	public string Cast { get; set; } = string.Empty;

	public int Views { get; set; } = default;

    public string Trailer { get; set; } = string.Empty;

    public ICollection<NameResponse> Names { get; set; } = default!;

	public ICollection<DescriptionResponse> Descriptions { get; set; } = default!;

	public ImageResponse Image { get; set; } = default!;

	public SubscriptionResponse? RequiredSubscription { get; set; } = default;
}
