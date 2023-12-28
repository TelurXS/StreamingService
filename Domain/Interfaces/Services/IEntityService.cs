using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IEntityService<T>
{
    GetResult<T> FindById(Guid id);

    GetAllResult<T> FindAll();

    CreateResult<T> Create(T value);

    UpdateResult<T> Update(Guid id, T value);

    DeleteResult Delete(T value);
}