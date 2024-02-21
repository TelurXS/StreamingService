using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITitleRepository : IRepository<Title>
{
	Title? FindBySlug(string slug);

	Title? FindBySlugWithTracking(string slug);

	List<Title> FindAllPopular(int count = 10, int page = 0);

	List<Title> FindAllByName(string name, int count = 10, int page = 0);

	List<Title> FindAllByGenre(string genre, int count = 10, int page = 0);

	List<Title> FindAllByGenres(List<string> genres, int count = 10, int page = 0);

	bool SetImage(Guid id, Image image);

	bool AddScreenshot(Guid id, Image screenshot);

	bool RemoveScreenshot(Guid id, Image screenshot);

	bool AddView(Guid id, int count = 1);

	int CountByName(string name);

	int CountByGenre(string genre);

	int CountByGenres(List<string> genres);
}