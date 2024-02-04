using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class TitleService : ITitleService
{
    public TitleService(ITitleRepository repository)
    {
        Repository = repository;
    }
    
    private ITitleRepository Repository { get; }
    
    public GetResult<Title> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Title> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Title> FindBySlug(string slug)
	{
		var result = Repository.FindBySlug(slug);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Title> FindBySlugWithTracking(string slug)
	{
		var result = Repository.FindBySlugWithTracking(slug);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Title> FindAll()
    {
        return Repository.FindAll();
	}

	public GetAllResult<Title> FindAllWithTracking()
	{
		return Repository.FindAllWithTracking();
	}

	public CreateResult<Title> Create(Title value)
    {
        var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Title> Update(Guid id, Title value)
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

	public DeleteResult Delete(Title value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }
}