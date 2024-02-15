using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IViewRecordRepository : IRepository<ViewRecord>
{
	List<ViewRecord> FindAllFromUser(Guid userId);

	ViewRecord? FindBySeriesAndAuthor(Guid seriesId, User author);

	ViewRecord? FindBySeriesWithTracking(Guid seriesId);
}
