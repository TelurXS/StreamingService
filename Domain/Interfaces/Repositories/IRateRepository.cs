using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRateRepository : IRepository<Rate>
{
    List<Rate> FindAllByAuthor(Account account);
    
    List<Rate> FindAllByTitle(Title account);
}