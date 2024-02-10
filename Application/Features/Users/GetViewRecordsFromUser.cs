using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class GetViewRecordsFromUser
{
	public class Request : IRequest<GetAllResult<ViewRecord>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, GetAllResult<ViewRecord>>
	{
        public Handler(IUserService userService, IViewRecordService viewRecordService)
        {
			UserService = userService;
			ViewRecordService = viewRecordService;
		}

		private IUserService UserService { get; }
		private IViewRecordService ViewRecordService { get; }

		protected override GetAllResult<ViewRecord> Handle(Request request)
		{
			var userResult = UserService.FindById(request.UserId);

			if (userResult.IsFound is false)
				return new NotFound();

			return ViewRecordService.FindAllByUser(request.UserId);
		}
	}
}
