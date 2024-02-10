using Application.Features.TitleLists;
using Application.Features.TitlesLists;
using Domain.Entities;
using Domain.Models.Results;
using Domain.Models;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface ITitlesListService
{
	Task<GetResult<TitlesList>> GetById(Guid id);

	Task<CreateResult<TitlesList>> CreateAsync(CreateTitlesList.Request request);

	Task<UpdateResult<TitlesList>> Update(Guid id, UpdateTitlesList.Request request);

	Task<UpdateResult<Success>> AddTitleToListAsync(Guid listId, Title title);

	Task<UpdateResult<Success>> RemoveTitleFromListAsync(Guid listId, Title title);

	Task<DeleteResult> DeleteAsync(Guid id);
}
