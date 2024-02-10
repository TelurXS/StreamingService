namespace Domain.Entities;

public sealed class Series
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Uri { get; set; }
    
    public required string Dubbing { get; set; }
    
    public required int Index { get; set; }

	public Title Title { get; set; } = default!;
}