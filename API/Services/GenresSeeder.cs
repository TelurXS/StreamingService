
using Domain.Entities;
using Domain.Interfaces.Services;

namespace API.Services;

public sealed class GenresSeeder : BackgroundService
{
	public GenresSeeder(IServiceScopeFactory serviceScopeFactory)
	{
		ServiceScopeFactory = serviceScopeFactory;

		Genres = [
			"Action",
			"Adventure",
			"Comedy",
			"Drama",
			"Fantasy",
			"Horror",
			"Mystery",
			"Romance",
			"Sci-Fi",
			"Thriller",
			"Animation",
			"Documentary",
			"Family",
			"History",
			"Musical",
			"Sport",
			"War",
			"Western",
			"Crime",
			"Superhero",
			"Science Fiction",
			"Anime",
			"Manga",
			"Slice of Life",
			"Fantasy",
			"Shounen",
			"Shoujo",
			"Seinen",
			"Josei",
			"Isekai",
			"Cyberpunk",
			"Psychological",
			"Harem",
			"Mecha",
			"Romantic Comedy",
			"Supernatural",
			"TV Movie",
			"Action & Adventure",
			"Kids",
			"News",
			"Reality",
			"Sci-Fi & Fantasy",
			"Soap",
			"Talk",
			"War & Politics",
		];
	}

	private IServiceScopeFactory ServiceScopeFactory { get; }

	private List<string> Genres { get; }

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		using var scope = ServiceScopeFactory.CreateScope();
		var genresService = scope.ServiceProvider.GetRequiredService<IGenreService>();

		foreach (var name in Genres)
		{
			if (genresService.FindByName(name).IsFound)
				continue;

			var genre = new Genre { Name = name };

			genresService.Create(genre);
		}

		return Task.CompletedTask;
	}
}
