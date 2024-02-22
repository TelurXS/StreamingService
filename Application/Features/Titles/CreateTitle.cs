using Application.Features.Descriptions;
using Application.Features.Images;
using Application.Features.Names;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Titles;

public static class CreateTitle
{
	public class Request : IRequest<CreateResult<Title>>
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Slug { get; set; } = string.Empty;

		public float AvarageRate { get; set; } = default;

		public DateTime ReleaseDate { get; set; } = default;

		public TitleType Type { get; set; } = default;

		public Country Country { get; set; } = default;

        public AgeRestriction AgeRestriction { get; set; } = default;

		public string Director { get; set; } = string.Empty;

		public string Cast { get; set; } = string.Empty;

		public int Views { get; set; } = default;

		public string Trailer { get; set; } = string.Empty;

        public List<CreateName.Request> Names { get; set; } = new();

		public List<CreateDescription.Request> Descriptions { get; set; } = new();

		public List<string> GenresNames { get; set; } = new();
	}

    public class RequestValidator : AbstractValidator<Request> 
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(TitleConfiguration.NAME_MAX_LENGTH);

			RuleFor(x => x.Description)
			   .NotEmpty()
			   .MaximumLength(TitleConfiguration.DESCRIPTION_MAX_LENGTH);

			RuleFor(x => x.Slug)
			   .NotEmpty()
			   .MaximumLength(TitleConfiguration.SLUG_MAX_LENGTH);

            RuleFor(x => x.AvarageRate)
               .GreaterThanOrEqualTo(TitleConfiguration.AVARAGE_RATE_MIN)
               .LessThanOrEqualTo(TitleConfiguration.AVARAGE_RATE_MAX);

            RuleFor(x => x.ReleaseDate)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotNull();

            RuleFor(x => x.Country)
                .NotNull();

            RuleFor(x => x.AgeRestriction)
                .NotNull();

            RuleFor(x => x.Director)
			   .NotEmpty()
			   .MaximumLength(TitleConfiguration.DIRECTOR_MAX_LENGTH);

			RuleFor(x => x.Cast)
			   .NotEmpty()
			   .MaximumLength(TitleConfiguration.CAST_MAX_LENGTH);

			RuleFor(x => x.Views)
				.NotNull();

            RuleFor(x => x.Trailer)
                .NotNull()
				.MaximumLength(TitleConfiguration.TRAILER_MAX_LENGTH);

            RuleFor(x => x.Names)
				.NotNull();

			RuleFor(x => x.Descriptions)
				.NotNull();

			RuleFor(x => x.GenresNames)
				.NotNull();

			RuleForEach(x => x.Names)
				.SetValidator(new CreateName.RequestValidator());

			RuleForEach(x => x.Descriptions)
				.SetValidator(new CreateDescription.RequestValidator());
		}
    }

	public class Handler : IRequestHandler<Request, CreateResult<Title>>
	{
        public Handler(
			ITitleService titleService, 
			IGenreService genreService, 
			IValidator<Request> validator, 
			ITitleMapper titleMapper, 
			IMediator mediator)
        {
			TitleService = titleService;
			GenreService = genreService;
			Validator = validator;
			TitleMapper = titleMapper;
			Mediator = mediator;
		}

		private ITitleService TitleService { get; }
		private IGenreService GenreService { get; }
		private IValidator<Request> Validator { get; }
		private ITitleMapper TitleMapper { get; }
		private IMediator Mediator { get; }

		public async Task<CreateResult<Title>> Handle(Request request, CancellationToken cancellationToken)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var title = TitleMapper.FromRequest(request);

			title.Genres = [];

			foreach (var genreName in request.GenresNames)
			{
				var gerneResult = GenreService.FindByNameWithTracking(genreName);

				if (gerneResult.IsFound is false)
					continue;

				title.Genres.Add(gerneResult.AsFound);
			}

			return TitleService.Create(title);
		}
	}
}
