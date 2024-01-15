using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Rates;

public static class UpdateRate
{
    public class Request : IRequest<UpdateResult<Rate>>
    {
        public Guid Id { get; set; }

        public required float Value { get; set; }
    }
}
