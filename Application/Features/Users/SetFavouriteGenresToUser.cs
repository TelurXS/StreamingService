using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Users;

public static class SetFavouriteGenresToUser
{
	public class Request : IRequest<UpdateResult<Success>> 
	{
		public Guid UserId { get; set; } = default;

		public List<string> Genres { get; set; } = new();
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(IUserService userService, IGenreService genreService)
        {
			UserService = userService;
			GenreService = genreService;
		}

		private IUserService UserService { get; }
		private IGenreService GenreService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			var genres = new List<Genre>();
			
			foreach (var name in request.Genres)
			{
				var genreResult = GenreService.FindByNameWithTracking(name);

				if (genreResult.IsFound is false) 
					continue;

				genres.Add(genreResult.AsFound);
			}

			return UserService.SetFavouriteGenres(request.UserId, genres);
		}
	}
}
