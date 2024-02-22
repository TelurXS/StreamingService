using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Serieses;

public static class CreateSeries
{
    public class Request : IRequest<CreateResult<Series>>
    {
        public string Name { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;

        public string Dubbing { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public int Index { get; set; } = default;
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(SeriesConfiguration.NAME_MAX_LENGTH);

			RuleFor(x => x.Uri)
				.NotEmpty()
				.MaximumLength(SeriesConfiguration.URI_MAX_LENGTH);

			RuleFor(x => x.Dubbing)
				.NotEmpty()
				.MaximumLength(SeriesConfiguration.DUBBING_MAX_LENGTH);

			RuleFor(x => x.Language)
				.NotEmpty()
				.MaximumLength(SeriesConfiguration.LANGUAGE_MAX_LENGTH);

            RuleFor(x => x.Index)
                .NotNull();
		}
    }

	public class Handler : SyncRequestHandler<Request, CreateResult<Series>>
	{
		public Handler(
			ISeriesService seriesService, 
			IValidator<Request> validator,
			ISeriesMapper seriesMapper
			)
		{
			SeriesService = seriesService;
			Validator = validator;
			SeriesMapper = seriesMapper;
		}

		private ISeriesService SeriesService { get; }
		private IValidator<Request> Validator { get; }
		private ISeriesMapper SeriesMapper { get; }

		protected override CreateResult<Series> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var series = SeriesMapper.FromRequest(request);

			return SeriesService.Create(series);
		}
	}
}
