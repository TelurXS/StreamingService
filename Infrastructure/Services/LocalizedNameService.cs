using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class LocalizedNameService : ILocalizedNameService
{
    public LocalizedNameService(ILocalizedNameRepository repository)
    {
        Repository = repository;
    }
    
    private ILocalizedNameRepository Repository { get; }
    public GetResult<LocalizedName> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<LocalizedName> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<LocalizedName> Create(LocalizedName value)
    {
        return Repository.Insert(value);
    }

    public UpdateResult<LocalizedName> Update(Guid id, LocalizedName value)
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

    public DeleteResult Delete(LocalizedName value)
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