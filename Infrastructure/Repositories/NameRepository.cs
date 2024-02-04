using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class NameRepository : EntityRepository<Name>, INameRepository
{
    public NameRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Name? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .FirstOrDefault(x => x.Id == id);
    }

	public Name? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Title)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<Name> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .ToList();
	}

	public List<Name> FindAllWithTracking()
	{
		return Entities
			.Include(x => x.Title)
			.ToList();
	}

	public Name? Insert(Name value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return entity;

		return default;
	}

    public bool Update(Guid id, Name value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Language, x => value.Language)
                .SetProperty(x => x.Value, x => value.Value));

        return result > 0;
    }

    public bool Delete(Name value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }
}