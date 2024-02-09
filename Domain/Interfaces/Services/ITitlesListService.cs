using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface ITitlesListService : IEntityService<TitlesList>
{
	GetAllResult<TitlesList> FindAllByAuthor(User author);

	UpdateResult<Success> AddTitleToList(Guid id, Title title);

	UpdateResult<Success> RemoveTitleFromList(Guid id, Title title);
}
