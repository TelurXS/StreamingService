using Domain.Entities;

namespace Domain.Models.Responses;

public sealed class NotificationResponse
{
	public Guid Id { get; set; } = default;

	public string Message { get; set; } = string.Empty;

	public string LocalizabledMessage { get; set; } = string.Empty;

	public string? Link { get; set; } = default;

	public bool Snoozed { get; set; } = false;

	public DateTime Date { get; set; } = default;

	public UserResponse? RelatedUser { get; set; } = default;
}
