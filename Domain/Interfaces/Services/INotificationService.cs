using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface INotificationService : IEntityService<Notification>
{
	UpdateResult<Success> SnoozeAllByUser(Guid userId);
}
