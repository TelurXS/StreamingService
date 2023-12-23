namespace Domain.Entities;

public class Series
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Uri { get; set; }
}