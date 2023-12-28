using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class SeriesRepository : EntityRepository<Series>, ISeriesRepository
{
    public SeriesRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Series? FindById(Guid id)
    {
        return Entities
             .AsNoTracking()
             .Include(x => x.Title)
             .FirstOrDefault(x => x.Id == id);
    }

    public List<Series> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .ToList();
    }

    public Series Insert(Series value)
    {
        return Entities.Add(value).Entity;
    }

    public bool Update(Guid id, Series value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name)
                .SetProperty(x => x.Uri, x => value.Uri));

        return result > 0;
    }

    public bool Delete(Series value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }

    public List<Series> FindAllByTitle(Title title)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .Where(x => x.Title.Id == title.Id)
            .ToList();
    }
}