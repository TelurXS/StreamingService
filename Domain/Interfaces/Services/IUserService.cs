using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IUserService : IEntityService<User>
{
	GetAllResult<User> FindAllByName(string name, int count = 10, int page = 0);

	UpdateResult<Success> SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	UpdateResult<Success> SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);

	UpdateResult<Success> AddViewRecord(Guid id, ViewRecord viewRecord);

	GetAllResult<Title> FindFavouriteTitlesById(Guid id);

	UpdateResult<Success> AddTitleToFavourite(Guid id, Title title);

	UpdateResult<Success> RemoveTitleFromFavourite(Guid id, Title title);

	UpdateResult<Success> AddUserToFollowers(User follower, User user);

	UpdateResult<Success> RemoveUserFromFollowers(User follower, User user);

	GetAllResult<User> FindFollowersFromUser(Guid id);

	GetAllResult<User> FindReadersFromUser(Guid id);

	GetAllResult<Notification> FindNotificationsFromUser(Guid id);

	int CountByName(string name);
}
