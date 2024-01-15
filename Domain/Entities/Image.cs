namespace Domain.Entities;

public sealed class Image
{
    public Guid Id { get; set; }

    public required string Uri { get; set; }
}