using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Rates;

public static class CreateRate
{
    public class Request : IRequest<CreateResult<Rate>>
    {
        public Guid AccountId { get; set; }

        public Guid TitleId { get; set; }

        public required float Value { get; set; }
    }
}
