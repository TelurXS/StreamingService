using Domain.Entities;
using OneOf;

namespace Domain.Models.Results.Unions;


/// <summary>
/// Represents the result of creating operation.
/// Сan be one of <typeparamref name="T"/>, <see cref="ValidationFailed"/> or <see cref="Failed"/>
/// </summary>
[GenerateOneOf]
public partial class CreateResult<T> 
    : OneOfBase<T, ValidationFailed, Failed>
{
    public bool IsCreated => IsT0;
    public bool IsValidationFailed => IsT1;
    public bool IsFailed => IsT2;
    
    public T AsFound => AsT0;
    public ValidationFailed AsValidationFailed => AsT1;
    public Failed AsFailed => AsT2;
}