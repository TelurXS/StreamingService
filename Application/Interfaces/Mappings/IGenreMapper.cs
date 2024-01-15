using Application.Features.Genres;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface IGenreMapper
{
    Genre FromRequest(CreateGenre.Request request);

    Genre FromRequest(UpdateGenre.Request request);
}
