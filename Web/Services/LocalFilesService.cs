using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class LocalFilesService : ILocalFilesService
{
    public LocalFilesService(HttpClient client)
    {
		Client = client;
	}

	private HttpClient Client { get; }

	public async Task<GetResult<T>> ReadFromJsonAsync<T>(string path)
	{
		try
		{
			var response = await Client.GetAsync(path);

			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<T>();

			return new NotFound();
		}
		catch (Exception ex) 
		{
			Console.WriteLine(ex.Message);
			return new Failed(ex.Message);
		}
	}
}
