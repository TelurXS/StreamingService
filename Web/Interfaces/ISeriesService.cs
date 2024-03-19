using Application.Features.Serieses;
using Domain.Entities;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Microsoft.AspNetCore.Components.Forms;

namespace Web.Interfaces;

public interface ISeriesService : IWebService<Series, CreateSeries.Request, UpdateSeries.Request>
{
	Task<UpdateResult<Success>> UploadFileAsync(Guid id, IBrowserFile file);

	Task<UpdateResult<Success>> AddSeriesToTitle(Guid id, Guid titleId);
}
