using Domain.Models;

namespace Domain.Entities;

public sealed class Title
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Slug { get; set; }

    public required float AvarageRate { get; set; }
    
    public required DateTime ReleaseDate { get; set; }

    public required Country Country { get; set; }

    public required AgeRestriction AgeRestriction { get; set; }

    public required string Director { get; set; }

    public required string Cast { get; set; }

    public ICollection<Name> Names { get; set; } = default!;

    public ICollection<Description> Descriptions { get; set; } = default!;

    public Image Image { get; set; } = default!;

    public ICollection<Image> Screenshots { get; set; } = default!;

    public ICollection<Genre> Genres { get; set; } = default!;
    
    public ICollection<Series> Series { get; set; } = default!;
    
    public ICollection<Rate> Rates { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
}