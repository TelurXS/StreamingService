using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IViewRecordService : IEntityService<ViewRecord>
{
	GetAllResult<ViewRecord> FindAllByUser(Guid userId);

	GetResult<ViewRecord> FindBySeries(Guid seriesId);

	GetResult<ViewRecord> FindBySeriesWithTracking(Guid seriesId);
}
