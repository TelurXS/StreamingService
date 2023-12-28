using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class LocalizedDescriptionRepository : EntityRepository<LocalizedDescription>, ILocalizedDescriptionRepository
{
    public LocalizedDescriptionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public LocalizedDescription? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<LocalizedDescription> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .ToList();
    }

    public LocalizedDescription Insert(LocalizedDescription value)
    {
        return Entities.Add(value).Entity;
    }

    public bool Update(Guid id, LocalizedDescription value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Language, x => value.Language)
                .SetProperty(x => x.Value, x => value.Value));

        return result > 0;
    }

    public bool Delete(LocalizedDescription value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }
}