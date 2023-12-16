using Domain.Interfaces.Results;

namespace Domain.Models.Results;

/// <summary>
/// Represents a Not Found result when no result was found.
/// </summary>
public sealed class NotFound : IResult
{
    public string Type => nameof(NotFound);
}