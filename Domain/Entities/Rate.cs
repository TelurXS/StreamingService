namespace Domain.Entities;

public class Rate
{
    public required Guid Id { get; set; }
    
    public required float Value { get; set; }
    
    public required Account Author { get; set; }
    
    public required Title Title { get; set; }
}