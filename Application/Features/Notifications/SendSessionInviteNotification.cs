
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Notifications;

public class SendSessionInviteNotification
{
	public class Request : IRequest<CreateResult<Notification>>
	{
		[JsonIgnore]
		public Guid ReceiverId { get; set; }

		[JsonIgnore]
		public Guid? AuthorId { get; set; }

		public string Link { get; set; } = string.Empty;
	}

	public class Handler : SyncRequestHandler<Request, CreateResult<Notification>>
	{
		public Handler(IUserService userService, INotificationService notificationService)
		{
			UserService = userService;
			NotificationService = notificationService;
		}

		private IUserService UserService { get; }
		private INotificationService NotificationService { get; }

		protected override CreateResult<Notification> Handle(Request request)
		{
			var receiverResult = UserService.FindByIdWithTracking(request.ReceiverId);

			if (receiverResult.IsFound is false)
				return new Failed();

			User? author = null;

			if (request.AuthorId is not null)
			{
				var authorResult = UserService.FindByIdWithTracking(request.AuthorId.Value);

				if (authorResult.IsFound)
					author = authorResult.AsFound;
			}
			
			var receiver = receiverResult.AsFound;

			var notification = new Notification
			{
				Id = Guid.NewGuid(),
				Message = "You have been invited to Watch Together Session",
				LocalizabledMessage = "SESSION_INVITE_NOTIFICATION",
				Link = request.Link,
				Snoozed = false,
				Date = DateTime.Now,
				Receiver = receiver,
				RelatedUser = author,
			};

			return NotificationService.Create(notification);
		}
	}
}
