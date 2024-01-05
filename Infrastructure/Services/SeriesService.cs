using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public class SeriesService : ISeriesService
{
    public SeriesService(ISeriesRepository repository)
    {
        Repository = repository;
    }
    
    private ISeriesRepository Repository { get; }
    
    public GetResult<Series> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Series> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<Series> Create(Series value)
    {
        return Repository.Insert(value);
    }

    public UpdateResult<Series> Update(Guid id, Series value)
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

    public DeleteResult Delete(Series value)
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

    public GetAllResult<Series> FindAllByTitle(Title title)
    {
        var result = Repository.FindAllByTitle(title);

        if (result is null)
            return new NotFound();

        return result;
    }
}