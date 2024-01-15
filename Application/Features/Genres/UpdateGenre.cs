using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Genres;

public static class UpdateGenre
{
    public class Request : IRequest<UpdateResult<Genre>>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
    }
}
