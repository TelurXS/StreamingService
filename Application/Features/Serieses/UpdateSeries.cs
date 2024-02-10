using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class UpdateSeries
{
    public class Request : IRequest<UpdateResult<Series>>
    {
        public Guid Id { get; set; } = default;

        public string Name { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;

        public string Dubbing { get; set; } = string.Empty;

        public int Index { get; set; } = default;
    }
}
