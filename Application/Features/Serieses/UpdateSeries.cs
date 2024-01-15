using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class UpdateSeries
{
    public class Request : IRequest<UpdateResult<Series>>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Uri { get; set; }
    }
}
