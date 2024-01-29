namespace Domain.Entities;

public sealed class Subscription
{
    public static readonly string Trial = nameof(Trial);

    public static readonly string Base = nameof(Base);

    public static readonly string Premium = nameof(Premium);

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required float Price { get; set; }
}
