using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public ICollection<Rate> Rates { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;

    public Subscription? Subscription { get; set; } = default;

    public DateTime? SubscriptionExpiresIn { get; set; } = default;
}
