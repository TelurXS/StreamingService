using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IWebService<T>
{
	Task<GetResult<T>> FindByIdAsync(Guid id);

	Task<GetAllResult<T>> FindAllAsync();

	Task<CreateResult<T>> CreateAsync(T value);

	Task<UpdateResult<T>> UpdateAsync(Guid id, T value);

	Task<DeleteResult> DeleteByIdAsync(Guid id);

	Task<DeleteResult> DeleteAsync(T value);
}
