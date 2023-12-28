namespace Domain.Entities;

public class Title
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Slug { get; set; }
    
    public required DateTime ReleaseDate { get; set; }

    public Image Image { get; set; } = default!;

    public ICollection<Image> Screenshots { get; set; } = default!;

    public ICollection<Genre> Genres { get; set; } = default!;
    
    public ICollection<Series> Series { get; set; } = default!;
    
    public ICollection<Rate> Rates { get; set; } = default!;

    public ICollection<LocalizedName> LocalizedNames { get; set; } = default!;

    public ICollection<LocalizedDescription> LocalizedDescriptions { get; set; } = default!;
}