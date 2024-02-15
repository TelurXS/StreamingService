using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Infrastructure.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class RegisterRateToTitleFromUser
{
	public class Request : IRequest<CreateResult<Success>>
	{
		public float Value { get; set; } = default;

		[JsonIgnore]
		public Guid UserId { get; set; } = default;

		[JsonIgnore]
		public Guid TitleId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, CreateResult<Success>>
	{
        public Handler(
			IUserService userService,
			IRateService rateService,
			ITitleService titleService)
        {
			UserService = userService;
			RateService = rateService;
			TitleService = titleService;
		}

		private IUserService UserService { get; }
		private IRateService RateService { get; }
		private ITitleService TitleService { get; }

		protected override CreateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindByIdWithTracking(request.UserId);

			if (userResult.IsFound is false)
				return new Failed();

			var user = userResult.AsFound;

			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

			var rateResult = RateService.FindByTitleAndAuthor(title, user);

			if (rateResult.IsFound)
			{
				var rate = rateResult.AsFound;

				rate.Value = request.Value;

				var result = RateService.Update(rate.Id, rate);

				if (result.IsUpdated)
					return new Success();
			}
			else
			{
				var rate = new Rate
				{
					Value = request.Value,
					Author = user,
					Title = title,
				};

				var result = RateService.Create(rate);

				if (result.IsCreated)
					return new Success();
			}

			return new Failed();
		}
	}
}
