using Domain.Interfaces.Results;

namespace Domain.Models.Results;

/// <summary>
/// Represents a Failed result when something bad happened while processing request.
/// Contains a list of string errors.
/// </summary>
public sealed class Failed : IResult
{
    public Failed(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public Failed(string error)
    {
        Errors = new[] { error };
    }

    public Failed()
    {
        Errors = Enumerable.Empty<string>();
    }
    
    public IEnumerable<string> Errors { get; }
    
    public string Type => nameof(Failed);
}
