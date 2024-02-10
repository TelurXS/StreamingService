namespace Domain.Entities;

public sealed class Rate
{
    public Guid Id { get; set; }
    
    public required float Value { get; set; }

	public User Author { get; set; } = default!;

	public Title Title { get; set; } = default!;
}