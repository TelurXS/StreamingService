using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ISeriesRepository : IRepository<Series>
{
    List<Series> FindAllByTitle(Title title);
}