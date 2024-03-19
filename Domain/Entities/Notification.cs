namespace Domain.Entities;

public sealed class Notification
{
	public Guid Id { get; set; }

	public required string Message { get; set; }

	public required string LocalizabledMessage { get; set; }

	public required string? Link { get; set; }

	public required bool Snoozed { get; set; }

	public required DateTime Date { get; set; }

	public User Receiver { get; set; } = default!;

	public User? RelatedUser { get; set; } = default!;
}
