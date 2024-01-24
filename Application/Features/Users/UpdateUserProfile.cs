using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Users;

public static class UpdateUserProfile
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		public Guid Id { get; set; }

		public required string Name { get; set; }

		public required string Surname { get; set; }

		public required DateTime BirthDate { get; set; }
	}

	public class RequestValidator : AbstractValidator<Request> 
	{
		private DateTime MinDate => DateTime.Now.AddYears(-100);

		private DateTime MaxDate => DateTime.Now.AddYears(-10);

        public RequestValidator()
        {
			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(UserConfiguration.NAME_MAX_LENGTH);

			RuleFor(x => x.Surname)
				.NotEmpty()
				.MaximumLength(UserConfiguration.SURNAME_MAX_LENGTH);

			RuleFor(x => x.BirthDate)
				.NotNull()
				.LessThan(x => MaxDate)
				.GreaterThan(x => MinDate);
		}
    }

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
		public Handler(IUserService userService, IValidator<Request> validator)
		{
			UserService = userService;
			Validator = validator;
		}

		private IUserService UserService { get; }
		private IValidator<Request> Validator { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var userResult = UserService.FindById(request.Id);

			if (userResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;

			user.Name = request.Name;
			user.Surname = request.Surname;
			user.BirthDate = request.BirthDate;

			var result = UserService.Update(request.Id, user);

			if (result.IsFailed)
				return new Failed();

			return new Success();
		}
	}
}
