using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class TitlesListRepository : EntityRepository<TitlesList>, ITitlesListRepository
{
	public TitlesListRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public TitlesList? FindById(Guid id)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Names)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Id == id);
	}

	public TitlesList? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Names)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<TitlesList> FindAll(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Names)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Image)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<TitlesList> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Names)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Image)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<TitlesList> FindAllByAuthor(User author)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Names)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Titles)
				.ThenInclude(x => x.Image)
			.Where(x => x.Author.Id == author.Id)
			.ToList();
	}

	public bool AddTitleToList(Guid id, Title title)
	{
		var titlesList = FindByIdWithTracking(id);

		if (titlesList is null)
			return false;

		titlesList.Titles.Add(title);
		return Context.SaveChanges() > 0;
	}

	public bool RemoveTitleFromList(Guid id, Title title)
	{
		var titlesList = FindByIdWithTracking(id);

		if (titlesList is null)
			return false;

		titlesList.Titles.Remove(title);
		return Context.SaveChanges() > 0;
	}

	public TitlesList? Insert(TitlesList value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

	public bool Update(Guid id, TitlesList value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Name, x => value.Name)
				.SetProperty(x => x.Availability, x => value.Availability)
				);

		return result > 0;
	}

	public bool Delete(TitlesList value)
	{
		Entities.Remove(value);
		return Context.SaveChanges() > 0;
	}

	public int Count()
	{
		return Entities.Count();
	}
}
