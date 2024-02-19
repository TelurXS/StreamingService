using Domain.Entities;
using Domain.Models.Results.Unions;
using Domain.Models;
using MediatR;
using System.Text.Json.Serialization;
using Application.Models;
using Domain.Interfaces.Services;
using FluentValidation;
using Infrastructure.Configurations;
using Domain.Models.Results;
using Application.Interfaces.Mappings;

namespace Application.Features.TitleLists;

public static class CreateTitlesList
{
	public class Request : IRequest<CreateResult<TitlesList>>
	{
		public string Name { get; set; } = string.Empty;

		public Availability Availability { get; set; } = default;

		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class RequestValidator : AbstractValidator<Request> 
	{
        public RequestValidator()
        {
			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(TitlesListConfiguration.NAME_MAX_LENGTH);
        }
    }

	public class Handler : SyncRequestHandler<Request, CreateResult<TitlesList>>
	{
        public Handler(
			ITitlesListService titlesListService, 
			IUserService userService,
			IValidator<Request> validator, 
			ITitlesListMapper mapper)
        {
			TitlesListService = titlesListService;
			UserService = userService;
			Validator = validator;
			Mapper = mapper;
		}

		private ITitlesListService TitlesListService { get; }
		private IUserService UserService { get; }
		private IValidator<Request> Validator { get; }
		private ITitlesListMapper Mapper { get; }

		protected override CreateResult<TitlesList> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var authorResult = UserService.FindByIdWithTracking(request.UserId);

			if (authorResult.IsFound is false)
				return new Failed();

			var titlesList = Mapper.FromRequest(request);

			titlesList.Author = authorResult.AsFound;

			return TitlesListService.Create(titlesList);
		}
	}
}
