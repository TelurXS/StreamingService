namespace Domain.Models.Responses;

public sealed class ClaimsResponse 
{
	public List<ClaimResponse> Claims { get; set; } = new();
};
