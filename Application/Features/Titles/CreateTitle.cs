using Domain.Entities;
using Domain.Models;
using Domain.Models.Results.Unions;
using MediatR;

namespace Application.Features.Titles;

public static class CreateTitle
{
    public class Request : IRequest<CreateResult<Title>>
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Slug { get; set; }

        public required float AvarageRate { get; set; }

        public required DateTime ReleaseDate { get; set; }

        public required Country Country { get; set; }

        public required AgeRestriction AgeRestriction { get; set; }

        public required string Director { get; set; }

        public required string Cast { get; set; }

        public required int Views { get; set; }
    }
}
