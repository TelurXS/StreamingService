using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class SubscriptionService : ISubscriptionService
{
    public SubscriptionService(ISubscriptionRepository repository)
    {
        Repository = repository;
    }

    private ISubscriptionRepository Repository { get; }

    public GetResult<Subscription> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetResult<Subscription> FindByIdWithTracking(Guid id)
    {
        var result = Repository.FindByIdWithTracking(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetResult<Subscription> FindByName(string name)
    {
        var result = Repository.FindByName(name);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetResult<Subscription> FindByNameWithTracking(string name)
    {
        var result = Repository.FindByNameWithTracking(name);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Subscription> FindAll(int count = 10, int page = 0)
    {
        return Repository.FindAll(count, page);
    }

    public GetAllResult<Subscription> FindAllWithTracking(int count = 10, int page = 0)
    {
        return Repository.FindAllWithTracking(count, page);
    }

    public CreateResult<Subscription> Create(Subscription value)
    {
        var result = Repository.Insert(value);

        if (result is null)
            return new Failed();

        return result;
    }

    public UpdateResult<Subscription> Update(Guid id, Subscription value)
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

    public DeleteResult Delete(Subscription value)
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
