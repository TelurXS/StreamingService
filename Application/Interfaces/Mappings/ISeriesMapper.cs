using Application.Features.Serieses;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface ISeriesMapper
{
    Series FromRequest(CreateSeries.Request request);

    Series FromRequest(UpdateSeries.Request request);
}
