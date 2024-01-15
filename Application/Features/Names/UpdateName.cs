using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.LocalizedNames;

public static class UpdateName
{
    public class Request : IRequest<UpdateResult<Name>>
    {
        public Guid Id { get; set; }

        public required string Language { get; set; }

        public required string Value { get; set; }
    }
}
