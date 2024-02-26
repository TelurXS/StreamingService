using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Genres;

public static class GetGenreById
{
    public class Request : IRequest<GetResult<Genre>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : SyncRequestHandler<Request, GetResult<Genre>>
    {
        public Handler(IGenreService genreService)
        {
            GenreService = genreService;
        }

        private IGenreService GenreService { get; }

        protected override GetResult<Genre> Handle(Request request)
        {
            return GenreService.FindById(request.Id);
        }
    }
}
