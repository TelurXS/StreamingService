namespace Domain.Models.Responses;

public sealed class UserResponse
{
	public Guid Id { get; set; } = default;

	public string Email { get; set; } = string.Empty;

	public string PhoneNumber { get; set; } = string.Empty;

	public string UserName { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public string Surname { get; set; } = string.Empty;

	public string ProfileImage { get; set; } = string.Empty;

	public DateTime BirthDate { get; set; } = default;
}
