using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {SecondName}";

    public string ProfileImage { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; } = default;

    public DateTime? SubscriptionExpiresIn { get; set; } = default;

    public bool IsTrialSubscriptionUsed { get; set; } = default;

	public Subscription? Subscription { get; set; } = default;

	public ICollection<Rate> Rates { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;

    public ICollection<Genre> FavouriteGenres { get; set; } = default!;

    public ICollection<Title> FavouriteTitles { get; set; } = default!;

    public ICollection<ViewRecord> ViewRecords { get; set; } = default!;

    public ICollection<User> Readers { get; set; } = default!;

    public ICollection<User> Followers { get; set; } = default!;

    public ICollection<TitlesList> Lists { get; set; } = default!;

    public ICollection<Notification> Notifications { get; set; } = default!;
}
