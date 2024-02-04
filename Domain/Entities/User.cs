using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    //public string ProfileImage { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; } = default;

    public DateTime? SubscriptionExpiresIn { get; set; } = default;

	public Subscription? Subscription { get; set; } = default;

	public ICollection<Rate> Rates { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;

    public ICollection<Genre> FavouriteGenres { get; set; } = default!;
}
