using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IWebService<T, TCreate, TUpdate>
{
	Task<GetResult<T>> FindByIdAsync(Guid id);

	Task<GetAllResult<T>> FindAllAsync(int count = 10, int page = 0);

	Task<CreateResult<T>> CreateAsync(TCreate value);

	Task<UpdateResult<T>> UpdateAsync(Guid id, TUpdate value);

	Task<DeleteResult> DeleteByIdAsync(Guid id);

	Task<DeleteResult> DeleteAsync(T value);

    Task<int> CountAsync();
}
