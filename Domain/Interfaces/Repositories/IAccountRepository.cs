using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Account? FindByLogin(string login);

    Account? FindByEmail(string email);
}