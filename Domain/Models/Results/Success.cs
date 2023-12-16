using Domain.Interfaces.Results;

namespace Domain.Models.Results;

/// <summary>
/// Represents a Successful result when the operation was a success.
/// </summary>
public sealed class Success : IResult
{
    public string Type => nameof(Success);
}