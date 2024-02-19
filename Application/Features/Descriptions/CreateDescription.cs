using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Descriptions;

public static class CreateDescription
{
    public class Request : IRequest<CreateResult<Description>>
    {
        public required string Language { get; set; }

        public required string Value { get; set; }
    }

	public class RequestValidator : AbstractValidator<Request>
	{
		public RequestValidator()
		{
			RuleFor(x => x.Value)
				.NotEmpty()
				.MaximumLength(DescriptionConfiguration.VALUE_MAX_LENGTH);

			RuleFor(x => x.Language)
				.NotEmpty()
				.MaximumLength(DescriptionConfiguration.LANGUAGE_MAX_LENGTH);
		}
	}

	public class Handler : SyncRequestHandler<Request, CreateResult<Description>>
	{
		public Handler(
			IDescriptionService nameService,
			IDescriptionsMapper nameMapper,
			IValidator<Request> validator)
		{
			DescriptionService = nameService;
			DescriptionsMapper = nameMapper;
			Validator = validator;
		}

		private IDescriptionService DescriptionService { get; }
		private IDescriptionsMapper DescriptionsMapper { get; }
		private IValidator<Request> Validator { get; }

		protected override CreateResult<Description> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var description = DescriptionsMapper.FromRequest(request);

			return DescriptionService.Create(description);
		}
	}
}
