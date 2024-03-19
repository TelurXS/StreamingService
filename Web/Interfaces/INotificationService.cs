using Application.Features.Notifications;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Web.Interfaces
{
    public interface INotificationService
    {
        Task<CreateResult<Success>> SendInvite(SendSessionInviteNotification.Request request);
    }
}