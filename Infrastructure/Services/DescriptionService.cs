using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class DescriptionService : IDescriptionService
{
    public DescriptionService(IDescriptionRepository repository)
    {
        Repository = repository;
    }
    
    private IDescriptionRepository Repository { get; }
    
    public GetResult<Description> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Description> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Description> FindAll()
    {
        return Repository.FindAll();
	}

	public GetAllResult<Description> FindAllWithTracking()
	{
		return Repository.FindAllWithTracking();
	}

	public CreateResult<Description> Create(Description value)
    {
        var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Description> Update(Guid id, Description value)
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
		var account = Repository.FindById(id);

		if (account is null)
			return new NotFound();

		return Delete(account);
	}

	public DeleteResult Delete(Description value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }
}