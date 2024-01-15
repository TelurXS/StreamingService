namespace Domain.Entities;

public sealed class Name
{
    public Guid Id { get; set; }

    public required string Language { get; set; }

    public required string Value { get; set; }
    
    public Title Title { get; set; } = default!;
}