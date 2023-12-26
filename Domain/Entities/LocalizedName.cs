namespace Domain.Entities;

public class LocalizedName
{
    public required Guid Id { get; set; }

    public required string Language { get; set; }

    public required string Value { get; set; }
    
    public required Title Title { get; set; }
}