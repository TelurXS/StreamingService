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
            //.Include(x => x.Rates)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Account> FindAll()
    {
        return Entities
            .AsNoTracking()
            //.Include(x => x.Rates)
            .ToList();
    }
    
    public Account Insert(Account value)
    {
        return Entities.Add(value).Entity;
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

    public Account? FindByLogin(string login)
    {
        return Entities
            .AsNoTracking()
            //.Include(x => x.Rates)
            .FirstOrDefault(x => x.Login == login);
    }

    public Account? FindByEmail(string email)
    {
        return Entities
            .AsNoTracking()
            //.Include(x => x.Rates)
            .FirstOrDefault(x => x.Email == email);
    }
}