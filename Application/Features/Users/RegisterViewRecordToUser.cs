using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class RegisterViewRecordToUser
{
	public class Request : IRequest<CreateResult<Success>> 
	{
		public float Progress { get; set; } = default;
		
		public DateTime Time { get; set; } = DateTime.Now;

		[JsonIgnore]
		public Guid UserId { get; set; } = default;

		[JsonIgnore]
		public Guid SeriesId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, CreateResult<Success>>
	{
        public Handler(
			IUserService userService, 
			ISeriesService seriesService, 
			IViewRecordService viewRecordService)
        {
			UserService = userService;
			SeriesService = seriesService;
			ViewRecordService = viewRecordService;
		}

		private IUserService UserService { get; }
		private ISeriesService SeriesService { get; }
		private IViewRecordService ViewRecordService { get; }

		protected override CreateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindByIdWithTracking(request.UserId);

			if (userResult.IsFound is false)
				return new Failed();

			var user = userResult.AsFound;

			var seriesResult = SeriesService.FindByIdWithTracking(request.SeriesId);

			if (seriesResult.IsFound is false)
				return new Failed();

			var series = seriesResult.AsFound;

			var viewRecordResult = ViewRecordService.FindBySeries(request.SeriesId);

			if (viewRecordResult.IsFound)
			{
				var viewRecord = viewRecordResult.AsFound;

				viewRecord.Progress = request.Progress;

				var result = ViewRecordService.Update(viewRecord.Id, viewRecord);

				if (result.IsUpdated)
					return new Success();
			}
			else
			{
				var viewRecord = new ViewRecord
				{
					Author = user,
					Series = series,
					Title = series.Title,
					Progress = request.Progress,
					Time = DateTime.Now
				};

				var result = UserService.AddViewRecord(user.Id, viewRecord);

				if (result.IsUpdated)
					return new Success();
			}
		
			return new Failed();
		}
	}
}
