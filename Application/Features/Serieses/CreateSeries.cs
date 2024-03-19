using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using Infrastructure.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Serieses;

public static class CreateSeries
{
    public class Request : IRequest<CreateResult<Series>>
    {
		public Guid TitleId { get; set; } = default;

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
				.NotNull()
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
			ITitleService titleService, 
			IValidator<Request> validator,
			ISeriesMapper seriesMapper
			)
		{
			SeriesService = seriesService;
			TitleService = titleService;
			Validator = validator;
			SeriesMapper = seriesMapper;
		}

		private ISeriesService SeriesService { get; }
		private ITitleService TitleService { get; }
		private IValidator<Request> Validator { get; }
		private ISeriesMapper SeriesMapper { get; }

		protected override CreateResult<Series> Handle(Request request)
		{
			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

            var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var series = SeriesMapper.FromRequest(request);
			series.Title = title;

			return SeriesService.Create(series);
		}
	}
}
