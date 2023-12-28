namespace Domain.Entities;

public class Rate
{
    public Guid Id { get; set; }
    
    public required float Value { get; set; }
    
    public Account Author { get; set; } = default!;
    
    public Title Title { get; set; } = default!;
}