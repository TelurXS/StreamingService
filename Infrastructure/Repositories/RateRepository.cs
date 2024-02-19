using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RateRepository : EntityRepository<Rate>, IRateRepository
{
    public RateRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Rate? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
            .FirstOrDefault(x => x.Id == id);
	}

	public Rate? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Title)
			.FirstOrDefault(x => x.Id == id);
	}

	public Rate? FindByTitleAndAuthor(Title title, User author)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Title)
			.FirstOrDefault(x => x.Title.Id == title.Id && x.Author.Id == author.Id);
	}

	public Rate? FindByTitleAndAuthorWithTracking(Title title, User author)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Title)
			.FirstOrDefault(x => x.Id == title.Id && x.Author.Id == author.Id);
	}

	public List<Rate> FindAll(int count = 10, int page = 0)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Title)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Rate> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Rate> FindAllByAuthor(Account account)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Where(x => x.Author.Id == account.Id)
			.ToList();
	}

	public List<Rate> FindAllByTitle(Title title)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Where(x => x.Title.Id == title.Id)
			.ToList();
	}

	public Rate? Insert(Rate value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

    public bool Update(Guid id, Rate value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Value, x => value.Value));

        return result > 0;
    }

    public bool Delete(Rate value)
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