using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IRateService : IWebService<Rate>
{
    GetAllResult<Rate> FindAllByAuthor(Account account);
    
    GetAllResult<Rate> FindAllByTitle(Title title);
}