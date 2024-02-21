using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    Subscription? FindByName(string name);

    Subscription? FindByNameWithTracking(string name);
}
