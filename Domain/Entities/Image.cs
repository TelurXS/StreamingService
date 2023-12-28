namespace Domain.Entities;

public class Image
{
    public Guid Id { get; set; }

    public required string Uri { get; set; }
}