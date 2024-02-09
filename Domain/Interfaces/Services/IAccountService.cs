using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IAccountService : IEntityService<Account>
{
    GetResult<Account> FindByLogin(string login);
    
    GetResult<Account> FindByEmail(string email);
}