namespace Domain.Entities;

public class LocalizedText
{
    public required Guid Id { get; set; }

    public required string Language { get; set; }

    public required string Value { get; set; }
}