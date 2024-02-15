using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Rates;

public static class GetRateByTitleAndAuthor
{
	public class Request : IRequest<GetResult<Rate>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;

		[JsonIgnore]
		public Guid TitleId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetResult<Rate>>
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

		protected override GetResult<Rate> Handle(Request request)
		{
			var userResult = UserService.FindByIdWithTracking(request.UserId);

			if (userResult.IsFound is false)
				return new Failed();

			var user = userResult.AsFound;

			var titleResult = TitleService.FindByIdWithTracking(request.TitleId);

			if (titleResult.IsFound is false)
				return new Failed();

			var title = titleResult.AsFound;

			return RateService.FindByTitleAndAuthor(title, user);
		}
	}
}
