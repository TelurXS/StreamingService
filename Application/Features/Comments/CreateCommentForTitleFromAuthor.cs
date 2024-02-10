using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Comments;

public static class CreateCommentForTitleFromAuthor
{
	public class Request : IRequest<CreateResult<Comment>>
	{
		public required string Content { get; set; }

		public required DateTime CreationDate { get; set; }

		[JsonIgnore]
		public Guid AuthorId { get; set; } = default;

		[JsonIgnore]
		public Guid TitleId { get; set; } = default;
	}

	public class RequestValidator : AbstractValidator<Request> 
	{
        public RequestValidator()
        {
			RuleFor(x => x.Content)
				.NotEmpty()
				.MaximumLength(CommentConfiguration.CONTENT_MAX_LENGTH);

			RuleFor(x => x.CreationDate)
				.LessThanOrEqualTo(x => DateTime.Now);
		}
    }

	public class Handler : SyncRequestHandler<Request, CreateResult<Comment>>
	{
        public Handler(
			IValidator<Request> validator,
			ICommentService commentService, 
			IUserService userService, 
			ITitleService titleService)
        {
			Validator = validator;
			CommentService = commentService;
			UserService = userService;
			TitleService = titleService;
		}

		private IValidator<Request> Validator { get; }
		private ICommentService CommentService { get; }
		private IUserService UserService { get; }
		private ITitleService TitleService { get; }

		protected override CreateResult<Comment> Handle(Request request)
		{
			var validationResult = Validator.Validate(request);

			if (validationResult.IsValid is false)
				return new ValidationFailed(validationResult.Errors);

			var authorResult = UserService.FindByIdWithTracking(request.AuthorId);

			if (authorResult.IsFound is false)
				return new Failed();

			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var comment = new Comment 
			{
				Content = request.Content,
				CreationDate = request.CreationDate,
				Author = authorResult.AsFound,
				Title = titleResult.AsFound,
			};

			var result = CommentService.Create(comment);

			return result;
		}
	}
}
