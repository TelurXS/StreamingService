using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class AccountRepository : EntityRepository<Account>, IAccountRepository
{
    public AccountRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Account? FindById(Guid id)
    {
        return Entities
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
	}

	public Account? FindByIdWithTracking(Guid id)
	{
		return Entities
			.FirstOrDefault(x => x.Id == id);
	}

	public Account? FindByLogin(string login)
	{
		return Entities
			.AsNoTracking()
			.FirstOrDefault(x => x.Login == login);
	}

	public Account? FindByEmail(string email)
	{
		return Entities
			.AsNoTracking()
			.FirstOrDefault(x => x.Email == email);
	}

	public List<Account> FindAll()
    {
        return Entities
            .AsNoTracking()
            .ToList();
	}

	public List<Account> FindAllWithTracking()
	{
		return Entities
			.ToList();
	}

	public Account? Insert(Account value)
    {
		var entity = Entities.Add(value).Entity;

		var result = Context.SaveChanges();

		if (result > 0)
			return entity;

		return default;
	}

    public bool Update(Guid id, Account value)
    {
        var result = Entities
            .Where(x => x.Id == id)
            .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Name, x => value.Name)
                .SetProperty(x => x.Login, x => value.Login)
                .SetProperty(x => x.Email, x => value.Email));

        return result > 0;
    }

    public bool Delete(Account value)
    {
        var result = Entities
            .Where(x => x.Id == value.Id)
            .ExecuteDelete();

        return result > 0;
    }
}