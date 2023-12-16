namespace Domain.Entities;

public class Title
{
    public int Id { get; set; }

    public Image Image { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public ICollection<Image> Screenshots { get; set; } = null!;
}