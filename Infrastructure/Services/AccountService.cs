using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class AccountService : IAccountService
{
    public AccountService(IAccountRepository repository)
    {
        Repository = repository;
    }
    
    private IAccountRepository Repository { get; }

    public GetResult<Account> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Account> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Account> FindByLogin(string login)
	{
		var result = Repository.FindByLogin(login);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Account> FindByEmail(string email)
	{
		var result = Repository.FindByEmail(email);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Account> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
	}

	public GetAllResult<Account> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public CreateResult<Account> Create(Account value)
    {
        var result =  Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Account> Update(Guid id, Account value)
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

	public DeleteResult DeleteById(Guid id)
	{
		var account = Repository.FindById(id);

		if (account is null)
			return new NotFound();

		return Delete(account);
	}

	public DeleteResult Delete(Account value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

	public int Count()
	{
		return Repository.Count();
	}
}