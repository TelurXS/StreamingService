namespace Domain.Entities;

public sealed class Subscription
{
	public static readonly Subscription Trial = new Subscription 
    {
        Name = nameof(Trial),
        Price = 0f,
    };

	public static readonly Subscription Standart = new Subscription
	{
		Name = nameof(Standart),
		Price = 9.99f,
	};

	public static readonly Subscription Premium = new Subscription
	{
		Name = nameof(Premium),
		Price = 14.99f,
	};

	public Guid Id { get; set; }

    public required string Name { get; set; }

    public required float Price { get; set; }
}
