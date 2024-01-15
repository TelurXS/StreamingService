using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.LocalizedDescriptions;

public static class CreateDescription
{
    public class Request : IRequest<CreateResult<Description>>
    {
        public required string Language { get; set; }

        public required string Value { get; set; }
    }
}
