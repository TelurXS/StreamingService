using Application.Features.Genres;
using Domain.Entities;

namespace Web.Interfaces;

public interface IGenreService : IWebService<Genre, CreateGenre.Request, UpdateGenre.Request>
{
}
