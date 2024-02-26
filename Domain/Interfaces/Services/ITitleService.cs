using Domain.Entities;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Domain.Interfaces.Services;

public interface ITitleService : IEntityService<Title>
{
	GetResult<Title> FindBySlug(string slug);

	GetResult<Title> FindBySlugWithTracking(string slug);

	GetAllResult<Title> FindAllPopular(int count = 10, int page = 0);

	GetAllResult<Title> FindAllByType(TitleType type, int count = 10, int page = 0);

	GetAllResult<Title> FindAllByName(string name, int count = 10, int page = 0);

	GetAllResult<Title> FindAllByGenre(string genre, int count = 10, int page = 0);

	GetAllResult<Title> FindAllByGenres(List<string> genres, int count = 10, int page = 0);

	GetAllResult<Title> FilterAll(TitleType? type, string? name, List<string> genres, TitleSorting sorting, int count, int page);

	UpdateResult<Success> AddView(Guid id, int count = 1);

    UpdateResult<Success> SetImage(Guid id, Image image);

    UpdateResult<Success> AddScreenshot(Guid id, Image screenshot);

    UpdateResult<Success> RemoveScreenshot(Guid id, Image screenshot);

	int CountByType(TitleType type);

	int CountByName(string name);

	int CountByGenre(string genre);

	int CountByGenres(List<string> genres);

	int CountByFilter(TitleType? type, string? name, List<string> genres, TitleSorting sorting);

}