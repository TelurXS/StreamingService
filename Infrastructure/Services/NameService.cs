using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class NameService : INameService
{
    public NameService(INameRepository repository)
    {
        Repository = repository;
    }
    
    private INameRepository Repository { get; }

    public GetResult<Name> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Name> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Name> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
	}

	public GetAllResult<Name> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public CreateResult<Name> Create(Name value)
    {
        var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Name> Update(Guid id, Name value)
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

	public DeleteResult Delete(Name value)
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