using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class TitleRepository : EntityRepository<Title>, ITitleRepository
{
    public TitleRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Title? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => x.Screenshots)
            .Include(x => x.Genres)
            .Include(x => x.Series)
            .Include(x => x.Rates)
            .Include(x => x.LocalizedNames)
            .Include(x => x.LocalizedDescriptions)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Title> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => x.Screenshots)
            .Include(x => x.Genres)
            .Include(x => x.Series)
            .Include(x => x.Rates)
            .Include(x => x.LocalizedNames)
            .Include(x => x.LocalizedDescriptions)
            .ToList();
    }

    public Title Insert(Title value)
    {
        return Entities.Add(value).Entity;
    }

    public bool Update(Guid id, Title value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name)
                .SetProperty(x => x.Description, x => value.Description)
                .SetProperty(x => x.Slug, x => value.Slug)
                .SetProperty(x => x.ReleaseDate, x => value.ReleaseDate));

        return result > 0;
    }

    public bool Delete(Title value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }
}