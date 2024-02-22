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

	public Series? FindByIdWithTracking(Guid id)
	{
		return Entities
			 .Include(x => x.Title)
			 .FirstOrDefault(x => x.Id == id);
	}

	public List<Series> FindAll(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Title)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Series> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Title)
			.Skip(page * count)
			.Take(count)
			.ToList();
    }

    public List<Series> FindAllByTitle(Title title)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Title)
            .Where(x => x.Title.Id == title.Id)
            .ToList();
    }

    public Series? Insert(Series value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

	public bool Update(Guid id, Series value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Name, x => value.Name)
				.SetProperty(x => x.Uri, x => value.Uri)
				.SetProperty(x => x.Dubbing, x => value.Dubbing)
				.SetProperty(x => x.Language, x => value.Language)
				.SetProperty(x => x.Index, x => value.Index));

		return result > 0;
	}

	public bool Delete(Series value)
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