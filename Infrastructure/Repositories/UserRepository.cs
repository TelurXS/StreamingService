using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
	public UserRepository(DataContext dataContext) : base(dataContext)
	{
	}

	public bool Delete(User value)
	{
		var result = Entities
			.Where(x => x.Id == value.Id)
			.ExecuteDelete();

		return result > 0;
	}

	public List<User> FindAll()
	{
		return Entities
			.AsNoTracking()
			.Include(x => x.Subscription)
			.Include(x => x.Rates)
			.Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
			.ToList();
	}

	public User? FindById(Guid id)
	{
		return Entities
            .AsNoTracking()
            .Include(x => x.Subscription)
            .Include(x => x.Rates)
            .Include(x => x.Comments)
			.Include(x => x.FavouriteGenres)
            .FirstOrDefault(x => x.Id == id);
	}

	public User Insert(User value)
	{
		return Entities.Add(value).Entity;
	}

	public bool SetFavouriteGenres(Guid id, IEnumerable<Genre> genres)
	{
		var user = Entities
			.Include(x => x.FavouriteGenres)
			.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return false;

		user.FavouriteGenres.Clear();

		foreach (var genre in genres)
			user.FavouriteGenres.Add(genre);

		return Context.SaveChanges() > 0;
	}

	public bool SetSubscription(Guid id, Subscription subscription, DateTime expiresIn)
	{
		var user = Entities.FirstOrDefault(x => x.Id == id);

		if (user is null)
			return false;

		user.Subscription = subscription;
		user.SubscriptionExpiresIn = expiresIn;

		return Context.SaveChanges() > 0;
	}

	public bool Update(Guid id, User value)
	{
		var result = Entities
			.Where(x => x.Id == id)
			.ExecuteUpdate(setters => setters
				.SetProperty(x => x.Name, x => value.Name)
				.SetProperty(x => x.Surname, x => value.Surname)
				.SetProperty(x => x.BirthDate, x => value.BirthDate)
				.SetProperty(x => x.SubscriptionExpiresIn, x => value.SubscriptionExpiresIn));

		return result > 0;
	}
}
