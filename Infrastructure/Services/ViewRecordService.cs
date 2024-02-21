using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class ViewRecordService : IViewRecordService
{
	public ViewRecordService(IViewRecordRepository repository)
    {
		Repository = repository;
	}

	private IViewRecordRepository Repository { get; }

	public GetResult<ViewRecord> FindById(Guid id)
	{
		var result = Repository.FindById(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<ViewRecord> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<ViewRecord> FindBySeriesAndAuthor(Guid seriesId, User author)
	{
		var result = Repository.FindBySeriesAndAuthor(seriesId, author);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<ViewRecord> FindBySeriesWithTracking(Guid seriesId)
	{
		var result = Repository.FindByIdWithTracking(seriesId);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<ViewRecord> FindAll(int count = 10, int page = 0)
	{
		return Repository.FindAll(count, page);
	}

	public GetAllResult<ViewRecord> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public GetAllResult<ViewRecord> FindAllByUser(Guid userId)
	{
		return Repository.FindAllFromUser(userId);
	}

	public CreateResult<ViewRecord> Create(ViewRecord value)
	{
		var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

	public UpdateResult<ViewRecord> Update(Guid id, ViewRecord value)
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

	public DeleteResult Delete(ViewRecord value)
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
