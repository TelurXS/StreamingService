using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Serieses;

public static class CreateSeries
{
    public class Request : IRequest<CreateResult<Series>>
    {
        public string Name { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;

        public string Dubbing { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public int Index { get; set; } = default;
    }
}
