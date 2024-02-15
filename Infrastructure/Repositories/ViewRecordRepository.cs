using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ViewRecordRepository : EntityRepository<ViewRecord>, IViewRecordRepository
{
	public ViewRecordRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public ViewRecord? FindById(Guid id)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Id == id);
	}

	public ViewRecord? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Id == id);
	}

	public ViewRecord? FindBySeriesAndAuthor(Guid seriesId, User author)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Series.Id == seriesId && x.Author.Id == author.Id);
	}

	public ViewRecord? FindBySeriesWithTracking(Guid seriesId)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.FirstOrDefault(x => x.Series.Id == seriesId);
	}

	public List<ViewRecord> FindAll()
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.ToList();
	}

	public List<ViewRecord> FindAllWithTracking()
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.ToList();
	}

	public List<ViewRecord> FindAllFromUser(Guid userId)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Series)
			.Include(x => x.Title)
				.ThenInclude(x => x.Names)
			.Include(x => x.Title)
				.ThenInclude(x => x.Descriptions)
			.Include(x => x.Title)
				.ThenInclude(x => x.Image)
			.Where(x => x.Author.Id == userId)
			.ToList();
	}

	public ViewRecord? Insert(ViewRecord value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return entity;

		return default;
	}

	public bool Update(Guid id, ViewRecord value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Progress, x => value.Progress)
				.SetProperty(x => x.Time, x => value.Time));

		return result > 0;
	}

	public bool Delete(ViewRecord value)
	{
		var result = Entities
			.Where(x => x.Id == value.Id)
			.ExecuteDelete();

		return result > 0;
	}
}
