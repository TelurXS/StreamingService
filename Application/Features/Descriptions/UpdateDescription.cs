using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.LocalizedDescriptions;

public static class UpdateDescription
{
    public class Request : IRequest<UpdateResult<Description>>
    {
        public Guid Id { get; set; }

        public required string Language { get; set; }

        public required string Value { get; set; }
    }
}
