using Azure;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.PayPal;
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
            .Include(x => x.RequiredSubscription)
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
            .Include(x => x.RequiredSubscription)
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
            .Include(x => x.RequiredSubscription)
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
            .Include(x => x.RequiredSubscription)
            .Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.ThenInclude(x => x.Author)
			.FirstOrDefault(x => x.Slug == slug);
	}

	public List<Title> FindAll(int count = 10, int page = 0)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Names)
            .Include(x => x.Descriptions)
            .Include(x => x.Image)
            .Include(x => x.RequiredSubscription)
            .Include(x => x.Screenshots)
            .Include(x => x.Genres)
            .Include(x => x.Series)
            .Include (x => x.Comments)
			.ThenInclude(x => x.Author)
			.OrderBy(x => x.Views)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Title> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.RequiredSubscription)
			.Include(x => x.Screenshots)
			.Include(x => x.Genres)
			.Include(x => x.Series)
			.Include(x => x.Comments)
			.OrderByDescending(x => x.Views)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Title> FindAllPopular(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.RequiredSubscription)
			.Include(x => x.Genres)
			.OrderByDescending(x => x.Views)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Title> FindAllByName(string name, int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.RequiredSubscription)
			.Include(x => x.Genres)
			.OrderByDescending(x => x.Views)
			.Where(x => x.Name.Contains(name) || x.Names.Any(x => x.Value.Contains(name)))
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Title> FindAllByGenre(string genre, int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.RequiredSubscription)
			.Include(x => x.Genres)
			.OrderByDescending(x => x.Views)
			.Where(x => x.Genres.Any(x => x.Name.Equals(genre)))
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Title> FindAllByGenres(List<string> genres, int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Names)
			.Include(x => x.Descriptions)
			.Include(x => x.Image)
			.Include(x => x.RequiredSubscription)
			.Include(x => x.Genres)
			.OrderBy(x => x.Views)
			.Where(title => title.Genres.Any(genre => genres.Contains(genre.Name)))
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public bool SetImage(Guid id, Image image)
	{
		var title = FindByIdWithTracking(id);

		if (title is null)
			return false;

		title.Image = image;
		return Context.SaveChanges() > 0;
	}

	public bool AddScreenshot(Guid id, Image screenshot)
	{
		var title = FindByIdWithTracking(id);

		if (title is null)
			return false;

		title.Screenshots.Add(screenshot);
		return Context.SaveChanges() > 0;
	}

	public bool RemoveScreenshot(Guid id, Image screenshot)
	{
		var title = FindByIdWithTracking(id);

		if (title is null)
			return false;

		title.Screenshots.Remove(screenshot);
		return Context.SaveChanges() > 0;
    }

    public bool AddView(Guid id, int count = 1)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Views, x => x.Views + count));

        return result > 0;
    }

    public Title? Insert(Title value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

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

	public int Count()
	{
		return Entities.Count();
	}

	public int CountByName(string name)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Names)
			.Where(x => x.Name.Contains(name) || x.Names.Any(x => x.Value.Contains(name)))
			.Count();
	}

	public int CountByGenre(string genre)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Genres)
			.Where(x => x.Genres.Any(x => x.Name.Equals(genre)))
			.Count();
	}

	public int CountByGenres(List<string> genres)
	{
		return Entities
			.AsNoTracking()
			.AsSplitQuery()
			.Include(x => x.Genres)
			.Where(title => title.Genres.Any(genre => genres.Contains(genre.Name)))
			.Count();
	}
}