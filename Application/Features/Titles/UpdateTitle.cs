using Application.Features.Descriptions;
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
using System.Text.Json.Serialization;

namespace Application.Features.Titles;

public static class UpdateTitle
{
	public class Request : IRequest<UpdateResult<Title>>
	{
		[JsonIgnore]
		public Guid Id { get; set; } = default;

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

			RuleFor(x => x.GenresNames)
				.NotNull();
		}
	}

	public class Handler : IRequestHandler<Request, UpdateResult<Title>>
	{
		public Handler(
			ITitleService titleService,
			IValidator<Request> validator,
			ITitleMapper titleMapper)
		{
			TitleService = titleService;
			Validator = validator;
			TitleMapper = titleMapper;
		}

		private ITitleService TitleService { get; }
		private IValidator<Request> Validator { get; }
		private ITitleMapper TitleMapper { get; }

		public async Task<UpdateResult<Title>> Handle(Request request, CancellationToken cancellationToken)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var titleResult = TitleService.FindById(request.Id);

			if (titleResult.IsFound is false)
				return new NotFound();

			var title = TitleMapper.FromRequest(request);

			return TitleService.Update(request.Id, title);
		}
	}
}
