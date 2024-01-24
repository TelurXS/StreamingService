using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Genre? FindByName(string name);

    Genre? FindByNameWithTracking(string name);
}