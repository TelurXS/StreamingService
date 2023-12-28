using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class GenreRepository : EntityRepository<Genre>, IGenreRepository
{
    public GenreRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Genre? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Titles)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Genre> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Titles)
            .ToList();
    }
    public Genre Insert(Genre value)
    {
        return Entities.Add(value).Entity;
    }

    public bool Update(Guid id, Genre value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name));

        return result > 0;
    }

    public bool Delete(Genre value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }

    public Genre? FindByName(string Name)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Titles)
            .FirstOrDefault(x => x.Name == Name);
    }
}