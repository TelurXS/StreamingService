using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRateRepository : IRepository<Rate>
{
    List<Rate> FindAllByAuthor(Account account);
    
    List<Rate> FindAllByTitle(Title title);

    Rate? FindByTitleAndAuthor(Title title, User author);

    Rate? FindByTitleAndAuthorWithTracking(Title title, User author);
}