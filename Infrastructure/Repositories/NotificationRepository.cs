using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class NotificationRepository : EntityRepository<Notification>, INotificationRepository
{
	public NotificationRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public Notification? FindById(Guid id)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Receiver)
			.Include(x => x.RelatedUser)
			.FirstOrDefault(x => x.Id == id);
	}

	public Notification? FindByIdWithTracking(Guid id)
	{
		return Entities
			.Include(x => x.Receiver)
			.Include(x => x.RelatedUser)
			.FirstOrDefault(x => x.Id == id);
	}

	public List<Notification> FindAll(int count = 10, int page = 0)
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Receiver)
			.Include(x => x.RelatedUser)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public List<Notification> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Entities
			.Include(x => x.Receiver)
			.Include(x => x.RelatedUser)
			.Skip(page * count)
			.Take(count)
			.ToList();
	}

	public bool SnoozeAllByUser(Guid userId)
	{
		var result = Entities
			.Include(x => x.Receiver)
			.Where(x => x.Receiver.Id == userId)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Snoozed, x => true)
				);

		return result > 0;
	}

	public Notification? Insert(Notification value)
	{
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return FindById(entity.Id);

		return default;
	}

	public bool Update(Guid id, Notification value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Message, x => value.Message)
				.SetProperty(x => x.LocalizabledMessage, x => value.LocalizabledMessage)
				.SetProperty(x => x.Link, x => value.Link)
				.SetProperty(x => x.Snoozed, x => value.Snoozed)
				.SetProperty(x => x.Date, x => value.Date)
				);

		return result > 0;
	}

	public bool Delete(Notification value)
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
