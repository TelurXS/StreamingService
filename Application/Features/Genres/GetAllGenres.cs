using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using Infrastructure.Services;
using MediatR;

namespace Application.Features.Genres;

public static class GetAllGenres
{
	public class Request : IRequest<GetAllResult<Genre>> 
	{
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<Genre>>
	{
		private const int COUNT = 100;

		public Handler(IGenreService genreService)
		{
			GenreService = genreService;
		}

		private IGenreService GenreService { get; }

		protected override GetAllResult<Genre> Handle(Request request)
		{
			return GenreService.FindAll(COUNT);
		}
	}
}
