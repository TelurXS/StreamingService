using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface ISeriesService : IWebService<Series>
{
    GetAllResult<Series> FindAllByTitle(Title title);
}