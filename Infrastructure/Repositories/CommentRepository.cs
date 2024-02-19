using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : EntityRepository<Comment>, ICommentRepository
{
	public CommentRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public Comment? FindById(Guid id)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Include(x => x.Parent)
			.Include(x => x.Childs)
			.FirstOrDefault(x => x.Id == id);
	}

	public Comment? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Include(x => x.Parent)
			.Include(x => x.Childs)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<Comment> FindAll(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Include(x => x.Parent)
			.Include(x => x.Childs)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Comment> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Author)
			.Include(x => x.Title)
			.Include(x => x.Parent)
			.Include(x => x.Childs)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public Comment? Insert(Comment value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

	public bool Update(Guid id, Comment value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Content, x => value.Content)
				.SetProperty(x => x.CreationDate, x => value.CreationDate)
				);

		return result > 0;
	}

	public bool Delete(Comment value)
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
