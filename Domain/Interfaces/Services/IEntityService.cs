using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IEntityService<T>
{
    GetResult<T> FindById(Guid id);

    GetResult<T> FindByIdWithTracking(Guid id);

    GetAllResult<T> FindAll(int count = 10, int page = 0);

    GetAllResult<T> FindAllWithTracking(int count = 10, int page = 0);

    CreateResult<T> Create(T value);

    UpdateResult<T> Update(Guid id, T value);

    DeleteResult DeleteById(Guid id);
    
    DeleteResult Delete(T value);

    int Count();
}