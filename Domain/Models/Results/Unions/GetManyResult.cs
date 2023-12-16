using OneOf;

namespace Domain.Models.Results.Unions;

/// <summary>
/// Represents the result of getting data.
/// Сan be one of <see cref="List&lt;T&gt;"/>, <see cref="NotFound"/> or <see cref="Failed"/>
/// </summary>
[GenerateOneOf]
public partial class GetManyResult<T> : OneOfBase<List<T>, NotFound, Failed>
{
    public bool IsFound => IsT0;
    public bool IsNotFound => IsT1;
    public bool IsFailed => IsT2;
    
    public List<T> AsFound => AsT0;
    public NotFound AsNotFound => AsT1;
    public Failed AsFailed => AsT2;
}