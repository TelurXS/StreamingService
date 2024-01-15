using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Genres;

public static class CreateGenre
{
    public class Request : IRequest<CreateResult<Genre>>
    {
        public required string Name { get; set; }
    }
}
