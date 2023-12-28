namespace Domain.Entities;

public class LocalizedName
{
    public Guid Id { get; set; }

    public required string Language { get; set; }

    public required string Value { get; set; }
    
    public Title Title { get; set; } = default!;
}