using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
	bool SnoozeAllByUser(Guid userId);
}
