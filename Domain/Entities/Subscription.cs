namespace Domain.Entities;

public sealed class Subscription
{
	public const string CURRENCY = "USD"; 
	public const int ACTIVE_DAYS = 30; 
	public const int TRIAL_ACTIVE_DAYS = 7; 

	public static readonly Subscription Trial = new Subscription 
    {
        Name = nameof(Trial),
        Price = 0f,
        Level = 0,
    };

	public static readonly Subscription Standart = new Subscription
	{
		Name = nameof(Standart),
		Price = 9.99f,
        Level = 1,
    };

	public static readonly Subscription Premium = new Subscription
	{
		Name = nameof(Premium),
		Price = 14.99f,
		Level = 2,
	};

	public static readonly List<Subscription> All = [Trial, Standart, Premium];

	public Guid Id { get; set; }

    public required string Name { get; set; }

    public required float Price { get; set; }

    public required int Level { get; set; }
}
