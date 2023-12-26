namespace Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    T? FindById(Guid id);

    List<T> FindAll();

    T Insert(T value);

    T? Update(Guid id, T value);

    bool Delete(T value);

    bool Delete(Guid id);
}