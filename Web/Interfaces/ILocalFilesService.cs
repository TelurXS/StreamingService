using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface ILocalFilesService
{
	Task<GetResult<T>> ReadFromJsonAsync<T>(string path);
}
