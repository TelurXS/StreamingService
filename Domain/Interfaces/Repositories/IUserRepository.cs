using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
	bool SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	bool SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);

	bool AddViewRecord(Guid id, ViewRecord viewRecord);

	List<Title> FindFavouriteTitlesById(Guid id);

	bool AddTitleToFavourite(Guid id, Title title);

	bool RemoveTitleFromFavourite(Guid id, Title title);

	bool AddUserToFollowers(Guid followerId, Guid userId);

	bool RemoveUserFromFollowers(Guid followerId, Guid userId);

	List<User> FindFollowersFromUser(Guid id);

	List<User> FindReadersFromUser(Guid id);

	List<Notification> FindNotificationsFromUser(Guid id);
}
