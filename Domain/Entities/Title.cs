namespace Domain.Entities;

public class Title
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Slug { get; set; }

    public required Image Image { get; set; }

    public required ICollection<Image> Screenshots { get; set; }

    public required ICollection<Series> Series { get; set; }

    public required ICollection<LocalizedText> LocalizedNames { get; set; }

    public required ICollection<LocalizedText> LocalizedDescriptions { get; set; }
}