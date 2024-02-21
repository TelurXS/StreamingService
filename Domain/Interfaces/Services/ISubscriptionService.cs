using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface ISubscriptionService : IEntityService<Subscription>
{
    GetResult<Subscription> FindByName(string name);

    GetResult<Subscription> FindByNameWithTracking(string name);
}
