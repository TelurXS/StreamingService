using Application.Interfaces.Mappings;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using Infrastructure.Services;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public static class UpdateTitlesList
{
	public class Request : IRequest<UpdateResult<TitlesList>>
	{
		public string Name { get; set; } = string.Empty;

		public Availability Availability { get; set; } = default;

		[JsonIgnore]
		public Guid Id { get; set; }
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

	public class Handler : SyncRequestHandler<Request, UpdateResult<TitlesList>>
	{
		public Handler(
		   ITitlesListService titlesListService,
		   IValidator<Request> validator,
		   ITitlesListMapper mapper)
		{
			TitlesListService = titlesListService;
			Validator = validator;
			Mapper = mapper;
		}

		private ITitlesListService TitlesListService { get; }
		private IValidator<Request> Validator { get; }
		private ITitlesListMapper Mapper { get; }

		protected override UpdateResult<TitlesList> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var titlesList = Mapper.FromRequest(request);

			return TitlesListService.Update(request.Id, titlesList);
		}
	}
}
