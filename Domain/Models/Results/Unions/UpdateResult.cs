using OneOf;

namespace Domain.Models.Results.Unions;

/// <summary>
/// Represents the result of updating operation.
/// Сan be one of <typeparamref name="T"/>, <see cref="NotFound"/>, <see cref="ValidationFailed"/> or <see cref="Failed"/>
/// </summary>
[GenerateOneOf]
public partial class UpdateResult<T> : OneOfBase<T, NotFound, ValidationFailed, Failed> 
{
    public bool IsUpdated => IsT0;
    public bool IsNotFound => IsT1;
    public bool IsValidationFailed => IsT2;
    public bool IsFailed => IsT3;
    
    public T AsUpdated => AsT0;
    public NotFound AsNotFound => AsT1;
    public ValidationFailed AsValidationFailed => AsT2;
    public Failed AsFailed => AsT3;
}