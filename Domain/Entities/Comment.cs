namespace Domain.Entities;

public sealed class Comment
{
    public Guid Id { get; set; }

    public required string Content { get; set; }

    public required DateTime CreationDate { get; set; }

    public User Author { get; set; } = default!;

    public Title Title { get; set; } = default!;

    public Comment? Parent { get; set; } = default;

    public ICollection<Comment>? Childs { get; set; } = default;
}
