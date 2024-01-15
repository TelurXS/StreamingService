using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class DescriptionService : IDescriptionService
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

    public GetAllResult<Description> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<Description> Create(Description value)
    {
        return Repository.Insert(value);
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

    public DeleteResult Delete(Description value)
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
}