using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IViewRecordRepository : IRepository<ViewRecord>
{
	List<ViewRecord> FindAllFromUser(Guid userId);

	ViewRecord? FindBySeries(Guid seriesId);

	ViewRecord? FindBySeriesWithTracking(Guid seriesId);
}
