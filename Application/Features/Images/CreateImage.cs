using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Images;

public static class CreateImage
{
    public class Request : IRequest<CreateResult<Image>>
    {
        public required string Uri { get; set; }
    }
}
