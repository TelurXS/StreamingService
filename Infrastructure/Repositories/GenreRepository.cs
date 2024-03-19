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

	public Genre? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Titles)
			.FirstOrDefault(x => x.Id == id);
	}

	public Genre? FindByName(string name)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Titles)
			.FirstOrDefault(x => x.Name == name);
	}

	public Genre? FindByNameWithTracking(string name)
	{
		return Entities
			.Include(x => x.Titles)
			.FirstOrDefault(x => x.Name == name);
	}

	public List<Genre> FindAll(int count = 10, int page = 0)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Titles)
			.Skip(page * count)
			.Take(count)
			.ToList();
    }

	public List<Genre> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
		   .Include(x => x.Titles)
		   .Skip(page * count)
			.Take(count)
		   .ToList();
	}

	public Genre? Insert(Genre value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
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
		Entities.Remove(value);
		return Context.SaveChanges() > 0;
	}

	public int Count()
	{
		return Entities.Count();
	}
}