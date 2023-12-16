using OneOf;

namespace Domain.Models.Results.Unions;

/// <summary>
/// Represents the result of deleting operation.
/// Сan be one of <see cref="Success"/>, <see cref="NotFound"/> or <see cref="Failed"/>
/// </summary>
[GenerateOneOf]
public partial class DeleteResult : OneOfBase<Success, NotFound, Failed>
{
    public bool IsSuccess => IsT0;
    public bool IsNotFound => IsT1;
    public bool IsFailed => IsT2;
    
    public Success AsSuccess => AsT0;
    public NotFound AsNotFound => AsT1;
    public Failed AsFailed => AsT2;
}