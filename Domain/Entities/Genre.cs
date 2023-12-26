namespace Domain.Entities;

public class Genre
{
    public required Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required ICollection<Title> Titles { get; set; }
}