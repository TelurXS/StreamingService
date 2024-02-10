namespace Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    T? FindById(Guid id);

    T? FindByIdWithTracking(Guid id);

    List<T> FindAll();

    List<T> FindAllWithTracking();

    T? Insert(T value);

    bool Update(Guid id, T value);

    bool Delete(T value);
}