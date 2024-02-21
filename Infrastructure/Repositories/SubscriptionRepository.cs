using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class SubscriptionRepository : EntityRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Subscription? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
    }

    public Subscription? FindByIdWithTracking(Guid id)
    {
        return Entities
            .FirstOrDefault(x => x.Id == id);
    }

    public Subscription? FindByName(string name)
    {
        return Entities
            .AsNoTracking()
            .FirstOrDefault(x => x.Name == name);
    }

    public Subscription? FindByNameWithTracking(string name)
    {
        return Entities
            .FirstOrDefault(x => x.Name == name);
    }

    public List<Subscription> FindAll(int count = 10, int page = 0)
    {
        return Entities
            .AsNoTracking()
			.Skip(page * count)
			.Take(count)
			.ToList();
    }

    public List<Subscription> FindAllWithTracking(int count = 10, int page = 0)
    {
        return Entities
			.Skip(page * count)
			.Take(count)
			.ToList();
    }

    public Subscription? Insert(Subscription value)
    {
        var entity = Entities.Add(value).Entity;

        var result = Context.SaveChanges();

        if (result > 0)
            return FindById(entity.Id);

        return default;
    }

    public bool Update(Guid id, Subscription value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name)
                .SetProperty(x => x.Price, x => value.Price)
                .SetProperty(x => x.Level, x => value.Level));

        return result > 0;
    }

    public bool Delete(Subscription value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }

	public int Count()
	{
		return Entities.Count();
	}
}
