namespace Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    T? FindById(Guid id);

    T? FindByIdWithTracking(Guid id);

    List<T> FindAll(int count = 10, int page = 0);

    List<T> FindAllWithTracking(int count = 10, int page = 0);

    T? Insert(T value);

    bool Update(Guid id, T value);

    bool Delete(T value);

    int Count();
}