using Domain.Interfaces.Results;
using FluentValidation.Results;

namespace Domain.Models.Results;

/// <summary>
/// Represents a Validation Failed result when there were validation errors in the sent request.
/// Contains a list of <see cref="ValidationFailure"/>.
/// </summary>
public sealed class ValidationFailed : IResult
{
	public ValidationFailed(IDictionary<string, string[]> errors)
	{
		Errors = errors
            .Select(x => 
            new ValidationFailure(x.Key, x.Value.FirstOrDefault()));
	}

	public ValidationFailed(IEnumerable<ValidationFailure> errors)
    {
        Errors = errors;
    }

    public ValidationFailed(ValidationFailure failure)
    {
        Errors = new[] { failure };
    }

    public ValidationFailed()
    {
        Errors = Enumerable.Empty<ValidationFailure>();
    }
    
    public IEnumerable<ValidationFailure> Errors { get; }
    
    public string Type => nameof(ValidationFailed);

}