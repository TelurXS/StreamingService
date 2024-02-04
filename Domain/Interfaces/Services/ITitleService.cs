using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface ITitleService : IWebService<Title>
{
	GetResult<Title> FindBySlug(string slug);

	GetResult<Title> FindBySlugWithTracking(string slug);
}