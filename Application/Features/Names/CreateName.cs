using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Names;

public static class CreateName
{
    public class Request : IRequest<CreateResult<Name>>
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
                .MaximumLength(NameConfiguration.VALUE_MAX_LENGTH);

			RuleFor(x => x.Language)
				.NotEmpty()
				.MaximumLength(NameConfiguration.LANGUAGE_MAX_LENGTH);
		}
    }

	public class Handler : SyncRequestHandler<Request, CreateResult<Name>>
	{
        public Handler(
            INameService nameService, 
            INameMapper nameMapper, 
            IValidator<Request> validator)
        {
			NameService = nameService;
			NameMapper = nameMapper;
			Validator = validator;
		}

		private INameService NameService { get; }
		private INameMapper NameMapper { get; }
		private IValidator<Request> Validator { get; }

		protected override CreateResult<Name> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var name = NameMapper.FromRequest(request);

			return NameService.Create(name);
		}
	}
}
