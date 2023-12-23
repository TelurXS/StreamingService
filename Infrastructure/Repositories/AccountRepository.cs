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

    private static readonly Func<DataContext, Guid, Account?> FindByIdQuery =
        EF.CompileQuery(
            (DataContext dataContext, Guid id) =>
                dataContext.Accounts.FirstOrDefault(x => x.Id == id));

    public Account? FindById(Guid id)
    {
        return Entities.FirstOrDefault(x => x.Id == id);
    }

    public List<Account> FindAll()
    {
        return Entities.ToList();
    }

    public Account? FindByIdWithInclude(Guid id)
    {
        return Entities.FirstOrDefault(x => x.Id == id);
    }

    public List<Account> FindAllWithInclude()
    {
        return Entities.ToList();
    }

    public Account Insert(Account value)
    {
        return Entities.Add(value).Entity;
    }

    public Account? Update(Guid id, Account value)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Account value)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Account? FindByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public Account? FindByEmail(string email)
    {
        throw new NotImplementedException();
    }
}