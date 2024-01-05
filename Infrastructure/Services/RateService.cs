using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class RateService : IRateService
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

    public GetAllResult<Rate> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<Rate> Create(Rate value)
    {
        return Repository.Insert(value);
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

    public DeleteResult Delete(Rate value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public DeleteResult DeleteById(Guid id)
    {
        var account = Repository.FindById(id);

        if (account is null)
            return new NotFound();

        return Delete(account);
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
}