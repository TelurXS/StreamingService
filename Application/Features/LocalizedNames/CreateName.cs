using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.LocalizedNames;

public static class CreateName
{
    public class Request : IRequest<CreateResult<LocalizedName>>
    {
        public required string Language { get; set; }

        public required string Value { get; set; }
    }
}
