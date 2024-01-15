using Application.Features.Genres;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class GenreMapper : IGenreMapper
{
    public partial Genre FromRequest(CreateGenre.Request request);

    public partial Genre FromRequest(UpdateGenre.Request request);
}