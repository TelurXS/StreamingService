using Domain.Entities;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Images;

public static class UpdateImage
{
    public class Request : IRequest<UpdateResult<Image>>
    {
        public Guid Id { get; set; }

        public required string Uri { get; set; }
    }
}
