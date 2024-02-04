using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITitleRepository : IRepository<Title>
{
	Title? FindBySlug(string slug);

	Title? FindBySlugWithTracking(string slug);
}