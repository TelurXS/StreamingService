using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class CreateSeries
{
    public class Request : IRequest<CreateResult<Series>>
    {
        public required string Name { get; set; }

        public required string Uri { get; set; }
    }
}
