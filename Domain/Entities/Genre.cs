namespace Domain.Entities;

public sealed class Genre
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

	public ICollection<Title> Titles { get; set; } = default!;

	public ICollection<User> FavouriteInUsers { get; set; } = default!;
}