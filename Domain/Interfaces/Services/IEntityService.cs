using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IWebService<T>
{
    GetResult<T> FindById(Guid id);

    GetResult<T> FindByIdWithTracking(Guid id);

    GetAllResult<T> FindAll();

    GetAllResult<T> FindAllWithTracking();

    CreateResult<T> Create(T value);

    UpdateResult<T> Update(Guid id, T value);

    DeleteResult DeleteById(Guid id);
    
    DeleteResult Delete(T value);
}