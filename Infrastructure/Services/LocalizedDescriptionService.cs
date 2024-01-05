using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class LocalizedDescriptionService : ILocalizedDescriptionService
{
    public LocalizedDescriptionService(ILocalizedDescriptionRepository repository)
    {
        Repository = repository;
    }
    
    private ILocalizedDescriptionRepository Repository { get; }
    
    public GetResult<LocalizedDescription> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<LocalizedDescription> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<LocalizedDescription> Create(LocalizedDescription value)
    {
        return Repository.Insert(value);
    }

    public UpdateResult<LocalizedDescription> Update(Guid id, LocalizedDescription value)
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

    public DeleteResult Delete(LocalizedDescription value)
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