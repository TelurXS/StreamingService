
namespace Domain.Models.Responses;

public class CommentResponse
{
	public Guid Id { get; set; } = default;

	public string Content { get; set; } = string.Empty;

	public DateTime CreationDate { get; set; } = default;

	public UserResponse Author { get; set; } = default!;
}
