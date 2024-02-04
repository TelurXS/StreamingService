namespace Domain.Models.Responses;

public class SubscriptionResponse
{
	public Guid Id { get; set; } = default;

	public string Name { get; set; } = string.Empty;

	public float Price { get; set; } = default;
}
