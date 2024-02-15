using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IViewRecordService : IEntityService<ViewRecord>
{
	GetAllResult<ViewRecord> FindAllByUser(Guid userId);

	GetResult<ViewRecord> FindBySeriesAndAuthor(Guid seriesId, User author);

	GetResult<ViewRecord> FindBySeriesWithTracking(Guid seriesId);
}
