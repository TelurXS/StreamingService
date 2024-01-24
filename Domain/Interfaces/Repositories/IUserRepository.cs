using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
	bool SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	bool SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);
}
