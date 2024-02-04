using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IUserService : IWebService<User>
{
	UpdateResult<Success> SetSubscription(Guid id, Subscription subscription, DateTime expiresIn);

	UpdateResult<Success> SetFavouriteGenres(Guid id, IEnumerable<Genre> genres);
}
