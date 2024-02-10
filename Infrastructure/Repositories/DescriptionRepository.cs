using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class DescriptionRepository : EntityRepository<Description>, IDescriptionRepository
{
    public DescriptionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Description? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .FirstOrDefault(x => x.Id == id);
    }

	public Description? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Title)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<Description> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .ToList();
    }

	public List<Description> FindAllWithTracking()
	{
		return Entities
			.Include(x => x.Title)
			.ToList();
	}

	public Description? Insert(Description value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return entity;

		return default;
	}

    public bool Update(Guid id, Description value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Language, x => value.Language)
                .SetProperty(x => x.Value, x => value.Value));

        return result > 0;
    }

    public bool Delete(Description value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }
}