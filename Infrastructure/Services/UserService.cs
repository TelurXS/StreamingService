using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class UserService : IUserService
{
    public UserService(IUserRepository repository)
    {
		Repository = repository;
	}

	private IUserRepository Repository { get; }

	public CreateResult<User> Create(User value)
	{
		return Repository.Insert(value);
	}

	public DeleteResult Delete(User value)
	{
		var result = Repository.Delete(value);

		if (result is false)
			return new Failed();

		return new Success();
	}

	public DeleteResult DeleteById(Guid id)
	{
		var account = Repository.FindById(id);

		if (account is null)
			return new NotFound();

		return Delete(account);
	}

	public GetAllResult<User> FindAll()
	{
		return Repository.FindAll();
	}

	public GetResult<User> FindById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public UpdateResult<User> Update(Guid id, User value)
	{
		if (Repository.FindById(id) is null)
			return new NotFound();

		if (Repository.Update(id, value) is false)
			return new Failed();

		var entity = Repository.FindById(id);

		if (entity is null)
			return new Failed();

		return entity;
	}


	public UpdateResult<Success> SetFavouriteGenres(Guid id, IEnumerable<Genre> genres)
	{
		var user = Repository.FindById(id);

		if (user is null)
			return new NotFound();

		var result = Repository.SetFavouriteGenres(id, genres);

		return new Success();
	}

	public UpdateResult<Success> SetSubscription(Guid id, Subscription subscription, DateTime expiresIn)
	{
		var user = Repository.FindById(id);

		if (user is null)
			return new NotFound();

		var result = Repository.SetSubscription(id, subscription, expiresIn);

		if (result is false)
			return new Failed();

		return new Success();
	}
}
