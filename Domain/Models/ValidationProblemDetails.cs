namespace Domain.Models;

public sealed class ValidationProblemDetails
{
	public IDictionary<string, string[]> Errors { get; set; } 
		= new Dictionary<string, string[]>(StringComparer.Ordinal);
}
