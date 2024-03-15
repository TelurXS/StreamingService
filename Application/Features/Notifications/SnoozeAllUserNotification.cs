using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Notifications;

public static class SnoozeAllUserNotification
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid UserId { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
        public Handler(IUserService userService, INotificationService notificationService)
        {
			UserService = userService;
			NotificationService = notificationService;
		}

		private IUserService UserService { get; }
		private INotificationService NotificationService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			if (UserService.FindById(request.UserId).IsFound is false)
				return new NotFound();

			return NotificationService.SnoozeAllByUser(request.UserId);
		}
	}
}
