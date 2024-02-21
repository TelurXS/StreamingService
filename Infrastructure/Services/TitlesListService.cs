using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class TitlesListService : ITitlesListService
{
    public TitlesListService(ITitlesListRepository repository)
    {
		Repository = repository;
	}

	private ITitlesListRepository Repository { get; }

	public GetResult<TitlesList> FindById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<TitlesList> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<TitlesList> FindAll(int count = 10, int page = 0)
	{
		return Repository.FindAll(count, page);
	}

	public GetAllResult<TitlesList> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public GetAllResult<TitlesList> FindAllByAuthor(User author)
	{
		return Repository.FindAllByAuthor(author);
	}

	public UpdateResult<Success> AddTitleToList(Guid id, Title title)
	{
		var result = Repository.AddTitleToList(id, title);

		if (result is false)
			return new Failed();

		return new Success();
	}

	public UpdateResult<Success> RemoveTitleFromList(Guid id, Title title)
	{
		var result = Repository.RemoveTitleFromList(id, title);

		if (result is false)
			return new Failed();

		return new Success();
	}

	public CreateResult<TitlesList> Create(TitlesList value)
	{
		var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

	public UpdateResult<TitlesList> Update(Guid id, TitlesList value)
	{
		if (Repository.FindById(id) is null)
			return new NotFound();

		if (Repository.Update(id, value) is false)
			return new Failed();

		var entity = Repository.FindById(id);

		if (entity is null)
			return new Failed();

		return entity;
	}

	public DeleteResult DeleteById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return Delete(result);
	}

	public DeleteResult Delete(TitlesList value)
	{
		var result = Repository.Delete(value);

		if (result is false)
			return new Failed();

		return new Success();
	}

	public int Count()
	{
		return Repository.Count();
	}
}
