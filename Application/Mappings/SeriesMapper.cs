using Application.Features.Serieses;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class SeriesMapper : ISeriesMapper
{
    public partial Series FromRequest(CreateSeries.Request request);

    public partial Series FromRequest(UpdateSeries.Request request);
}