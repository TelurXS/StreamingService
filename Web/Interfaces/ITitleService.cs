using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface ITitleService : IWebService<Title>
{
	Task<GetResult<Title>> FindBySlugAsync(string slug);
}