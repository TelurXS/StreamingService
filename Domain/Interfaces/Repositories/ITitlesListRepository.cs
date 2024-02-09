using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITitlesListRepository : IRepository<TitlesList>
{
	List<TitlesList> FindAllByAuthor(User author);

	bool AddTitleToList(Guid id, Title title);

	bool RemoveTitleFromList(Guid id, Title title);
}
