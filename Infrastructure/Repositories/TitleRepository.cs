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
            .Include(x => x.Names)
            .Include(x => x.Descriptions)
            .Include(x => x.Image)
            .Include(x => x.Screenshots)
            .Include(x => x.Genres)
            .Include(x => x.Series)
            .Include(x => x.Comments)
			.ThenInclude(x => x.Author)
            .FirstOrDefault(x => x.Id == id);
	}

	public Title? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.ThenInclude(x => x.Author)
			.FirstOrDefault(x => x.Id == id);
	}

	public Title? FindBySlug(string slug)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.ThenInclude(x => x.Author)
			.FirstOrDefault(x => x.Slug == slug);
	}

	public Title? FindBySlugWithTracking(string slug)
	{
		return Entities
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.ThenInclude(x => x.Author)
			.FirstOrDefault(x => x.Slug == slug);
	}

	public List<Title> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Names)
            .Include(x => x.Descriptions)
            .Include(x => x.Image)
            .Include(x => x.Screenshots)
            .Include(x => x.Genres)
            .Include(x => x.Series)
            .Include (x => x.Comments)
			.ThenInclude(x => x.Author)
			.ToList();
	}

	public List<Title> FindAllWithTracking()
	{
		return Entities
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.ThenInclude(x => x.Author)
			.ToList();
	}

	public Title? Insert(Title value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return entity;

		return default;
	}

    public bool Update(Guid id, Title value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name)
                .SetProperty(x => x.Description, x => value.Description)
                .SetProperty(x => x.Slug, x => value.Slug)
                .SetProperty(x => x.ReleaseDate, x => value.ReleaseDate)
                .SetProperty(x => x.Country, x => value.Country)
                .SetProperty(x => x.AgeRestriction, x => value.AgeRestriction)
                .SetProperty(x => x.Director, x => value.Director)
                .SetProperty(x => x.Cast, x => value.Cast)
                .SetProperty(x => x.Views, x => value.Views));

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