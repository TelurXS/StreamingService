namespace Domain.Entities;

public sealed class Subscription
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required float Price { get; set; }
}
