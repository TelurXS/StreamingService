using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class RateService : IRateService
{
    public RateService(IRateRepository repository)
    {
        Repository = repository;
    }
    
    private IRateRepository Repository { get; }
    
    public GetResult<Rate> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
	}

	public GetResult<Rate> FindByIdWithTracking(Guid id)
	{
		var result = Repository.FindByIdWithTracking(id);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Rate> FindByTitleAndAuthor(Title title, User author)
	{
		var result = Repository.FindByTitleAndAuthor(title, author);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetResult<Rate> FindByTitleAndAuthorWithTracking(Title title, User author)
	{
		var result = Repository.FindByTitleAndAuthorWithTracking(title, author);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Rate> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
	}

	public GetAllResult<Rate> FindAllWithTracking(int count = 10, int page = 0)
	{
		return Repository.FindAllWithTracking(count, page);
	}

	public GetAllResult<Rate> FindAllByAuthor(Account account)
	{
		var result = Repository.FindAllByAuthor(account);

		if (result is null)
			return new NotFound();

		return result;
	}

	public GetAllResult<Rate> FindAllByTitle(Title title)
	{
		var result = Repository.FindAllByTitle(title);

		if (result is null)
			return new NotFound();

		return result;
	}

	public CreateResult<Rate> Create(Rate value)
    {
        var result = Repository.Insert(value);

		if (result is null)
			return new Failed();

		return result;
	}

    public UpdateResult<Rate> Update(Guid id, Rate value)
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

	public DeleteResult Delete(Rate value)
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