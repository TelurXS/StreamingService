using Application.Features.Serieses;
using Domain.Entities;

namespace Web.Interfaces;

public interface ISeriesService : IWebService<Series, CreateSeries.Request, UpdateSeries.Request>
{
}
