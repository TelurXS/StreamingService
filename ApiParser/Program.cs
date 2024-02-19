
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Net.Http.Json;
using Domain.Models;
using System.Text.RegularExpressions;
using Application.Features.Titles;
using MediatR;
using Application.Features.Names;
using Application.Features.Descriptions;
using Microsoft.AspNetCore.Http;
using API.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Mappings;
using Application.Interfaces;
using Application.Mappings;
using Domain.Interfaces.Mappings;

const string API_KEY = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjYTU2NDdlNmU5MjJmMjg3NmUwYmQwODRmMWM0NGY2MyIsInN1YiI6IjY1ZDIxYjY5NmVlY2VlMDE4YTM5MmVlMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.SsGmEfVo6SaSVzjiQHrfyipG_bDbkL-DLwkQwiHiUEc";

var services = new ServiceCollection();

var connection = "Server=MYSQL6008.site4now.net;Database=db_aa5261_xflick;Uid=aa5261_xflick;Pwd=JA5d7g6T9z2C!yq";
var version = new MySqlServerVersion(new Version(8, 0, 35));

var configuration = new ConfigurationBuilder()
	.AddInMemoryCollection(new Dictionary<string, string>()
	{
		["Ftp:Host"] = "ftp://win6111.site4now.net",
		["Ftp:Username"] = "files_user",
		["Ftp:Password"] = "hLp@RgA6bQ!v3K",
	})
	.Build();

services.AddSingleton<IConfiguration>(configuration);

services.AddDbContext<DataContext>(options =>
	options.UseMySql(connection, version));

services.AddTransient<IAccountMapper, AccountMapper>();
services.AddTransient<IGenreMapper, GenreMapper>();
services.AddTransient<IImageMapper, ImageMapper>();
services.AddTransient<IDescriptionsMapper, DescriptionMapper>();
services.AddTransient<INameMapper, NameMapper>();
services.AddTransient<IRateMapper, RateMapper>();
services.AddTransient<ISeriesMapper, SeriesMapper>();
services.AddTransient<ITitleMapper, TitleMapper>();
services.AddTransient<IUserMapper, UserMapper>();
services.AddTransient<ITitlesListMapper, TitlesListMapper>();
services.AddTransient<IResponseMapper, ResponseMapper>();

services.AddTransient<IAccountRepository, AccountRepository>();
services.AddTransient<IDescriptionRepository, DescriptionRepository>();
services.AddTransient<IGenreRepository, GenreRepository>();
services.AddTransient<IImageRepository, ImageRepository>();
services.AddTransient<INameRepository, NameRepository>();
services.AddTransient<IRateRepository, RateRepository>();
services.AddTransient<ISeriesRepository, SeriesRepository>();
services.AddTransient<ITitleRepository, TitleRepository>();
services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<ICommentRepository, CommentRepository>();
services.AddTransient<IViewRecordRepository, ViewRecordRepository>();
services.AddTransient<ITitlesListRepository, TitlesListRepository>();
services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();

services.AddTransient<IAccountService, AccountService>();
services.AddTransient<IDescriptionService, DescriptionService>();
services.AddTransient<IGenreService, GenreService>();
services.AddTransient<IImageService, ImageService>();
services.AddTransient<INameService, NameService>();
services.AddTransient<IRateService, RateService>();
services.AddTransient<ISeriesService, SeriesService>();
services.AddTransient<ITitleService, TitleService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<IViewRecordService, ViewRecordService>();
services.AddTransient<ITitlesListService, TitlesListService>();
services.AddTransient<ISubscriptionService, SubscriptionService>();

services.AddTransient<IUnitOfWork, UnitOfWork>();
services.AddTransient<IFileService, FileService>();

services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

services.AddMediatR(x =>
	x.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

var provider = services.BuildServiceProvider();
IMediator mediator = provider.GetRequiredService<IMediator>();
IFileService fileServices = provider.GetRequiredService<IFileService>();

var client = new HttpClient();
client.BaseAddress = new Uri("https://api.themoviedb.org");
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

var imageClient = new HttpClient();
imageClient.BaseAddress = new Uri("https://image.tmdb.org");
imageClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);

var page = 10;

var respose = await client.GetAsync($"/3/tv/top_rated?language=en-US&page={page}");

var response = await respose.Content.ReadFromJsonAsync<responses>();

foreach (var result in response.results)
{
	var detailsResponse = await client.GetAsync($"/3/tv/{result.id}?language=en-US");

	if (detailsResponse.IsSuccessStatusCode is false)
		continue;

	var details = await detailsResponse.Content.ReadFromJsonAsync<details_response>();

	var slug = details.name
		.Replace(" ", "-")
		.Replace(":", "")
		.ToLower();

	if (Regex.IsMatch(slug, "^[a-zA-Z-_]*$") is false)
	{
		Console.WriteLine("---====================---");
		Console.WriteLine("INVALID NAME");
		Console.WriteLine(result.id);
		Console.WriteLine(slug);
		Console.WriteLine(details.name);
		Console.WriteLine("---====================---");
		continue;
	}

	var name = details.name;
	var description = details.overview;
	var avarageRate = details.vote_average;
	var releaseDate = details.first_air_date;
	var countryName = details.origin_country.FirstOrDefault() ?? "US";
	Enum.TryParse(countryName, out Country country);
	var type = TitleType.Series;
	var ageRestriction = details.adult ? AgeRestriction.AdultsOnly : AgeRestriction.Restricted;

	var directors = Enumerable.Empty<cast_response>();
	var cast = Enumerable.Empty<cast_response>();

	var names = new List<CreateName.Request>();
	var descriptions = new List<CreateDescription.Request>();

	var creditsResponse = await client.GetAsync($"/3/tv/{result.id}/credits?language=en-US");

	Console.WriteLine("---====================---");

	if (creditsResponse.IsSuccessStatusCode)
	{
		var credits = await creditsResponse.Content.ReadFromJsonAsync<credits_response>();

		directors = credits.crew.Where(x => x.known_for_department.Equals("Directing"));
		cast = credits.cast;
	}

	var translationResponse = await client.GetAsync($"/3/tv/{result.id}/translations");

	if (translationResponse.IsSuccessStatusCode)
	{
		var translation = await translationResponse.Content.ReadFromJsonAsync<translations_response>();

		foreach (var item in translation.translations)
		{
			if (item.iso_3166_1.Equals("RU"))
				continue;

			if (string.IsNullOrEmpty(item.data.name) is false)
			{
				names.Add(new CreateName.Request
				{
					Language = $"{item.iso_639_1}-{item.iso_3166_1}",
					Value = item.data.name,
				});
			}

			if (string.IsNullOrEmpty(item.data.overview) is false)
			{
				descriptions.Add(new CreateDescription.Request
				{
					Language = $"{item.iso_639_1}-{item.iso_3166_1}",
					Value = item.data.overview,
				});
			}
		}
	}

	Console.WriteLine(result.id);
	Console.WriteLine(name);
	Console.WriteLine(description);
	Console.WriteLine(slug);
	Console.WriteLine(avarageRate);
	Console.WriteLine(releaseDate);
	Console.WriteLine(country);
	Console.WriteLine(type);
	Console.WriteLine(ageRestriction);
	//Console.WriteLine("Genres: " + string.Join(", ", details.genres.Select(x => x.name).ToList()));
	Console.WriteLine("Directors: " + string.Join(", ", directors));
	Console.WriteLine("Cast: " + string.Join(", ", cast));

	//Console.WriteLine(string.Join(", ", names.Select(x => $"{x.Language}: {x.Value}")));
	//Console.WriteLine(string.Join(", ", descriptions.Select(x => $"{x.Language}: {x.Value}")));
	Console.WriteLine("---====================---");

	var request = new CreateTitle.Request
	{
		Name = name,
		Description = description,
		Slug = slug,
		AvarageRate = avarageRate,
		ReleaseDate = releaseDate,
		Country = country,
		Type = type,
		AgeRestriction = ageRestriction,
		Director = string.Join(", ", directors),
		Cast = string.Join(", ", cast),
		Views = Random.Shared.Next(0, 10000),
		Trailer = "",
		Names = names,
		Descriptions = descriptions,
		GenresNames = details.genres.Select(x => x.name).ToList(),
	};

	Console.WriteLine(details.poster_path);

	if ((await mediator.Send(new GetTitleBySlug.Request { Slug = slug })).IsFound)
		continue;

	var createResult = await mediator.Send(request);

	if (createResult.IsCreated)
	{
		var imageResponse = await imageClient.GetAsync($"/t/p/w600_and_h900_bestv2/{details.poster_path}");

		var imageStream = imageResponse.Content.ReadAsStream();
		var file = new FormFile(imageStream, 0, imageStream.Length, $"{Guid.NewGuid()}.jpg", $"{Guid.NewGuid()}.jpg");

		var setImageRequest = new SetImageToTitle.Request
		{
			File = file,
			TitleId = createResult.AsCreated.Id,
		};

		var setImageResult = await mediator.Send(setImageRequest);

		imageStream.Dispose();

		Console.WriteLine($"{slug} CREATED SUCCESSFULLY");
	}

}

class responses
{
	public int page { get; set; }
	public int total_pages { get; set; }
	public int total_results { get; set; }
	public List<short_response> results { get; set; } = new();
}

class short_response
{
	public int id { get; set; }
	public bool adult { get; set; }
	public List<int> genre_ids { get; set; } = new();
	public List<string> origin_country { get; set; } = new();

	public string original_name { get; set; }
	public string overview { get; set; }
	public float vote_average { get; set; }

}

public class details_response
{
	public bool adult { get; set; }
	
	public List<genre_response> genres { get; set; } = new();

	public List<string> origin_country { get; set; } = new();

	public int id { get; set; }

	public string name { get; set; }

	public string original_language { get; set; }
	public string original_name { get; set; }
	public string overview { get; set; }
	public string poster_path { get; set; }
	public DateTime first_air_date { get; set; }

	public float vote_average { get; set; }
}

public class genre_response
{
	public int id { get; set; }
	public string name { get; set; }
};

public class credits_response
{
	public List<cast_response> cast { get; set; }
	public List<cast_response> crew { get; set; }
}

public class cast_response
{
	public string known_for_department { get; set; }
	public string name { get; set; }
	public string original_name { get; set; }
	public string department { get; set; }

	public override string ToString() => name;
}

public class translations_response 
{
	public int id { get; set; }
	public List<translation_response> translations { get; set; }
}

public class translation_response
{
	public string iso_3166_1 { get; set; }
	public string iso_639_1 { get; set; }

	public translation_date_response data { get; set; }	
}

public class translation_date_response
{
	public string name { get; set; }
	public string overview { get; set; }
}

public class backdrops_response
{
	public List<backdrop> backdrops { get; set; }
}

public class backdrop
{
	public string file_path { get; set; }
}