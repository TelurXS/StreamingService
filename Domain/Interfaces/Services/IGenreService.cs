using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface IGenreService : IWebService<Genre>
{
    GetResult<Genre> FindByName(string name);

    GetResult<Genre> FindByNameWithTracking(string name);
}