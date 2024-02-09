using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
	bool SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	bool SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);

	bool AddViewRecord(Guid id, ViewRecord viewRecord);

	List<Title> FindFavouriteTitlesById(Guid id);

	bool AddTitleToFavourite(Guid id, Title title);

	bool RemoveTitleFromFavourite(Guid id, Title title);
}
