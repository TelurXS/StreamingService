using Application.Features.Notifications;
using Domain.Models;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using System.Net.Http.Json;
using Web.Interfaces;

namespace Web.Services;

public sealed class NotificationService : INotificationService
{
    public NotificationService(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<CreateResult<Success>> SendInvite(SendSessionInviteNotification.Request request)
    {
        try
        {
            var response = await Client
                .PostAsJsonAsync(ApiRoutes.Notifications.Invite + $"/{request.ReceiverId}", request);

            if (response.IsSuccessStatusCode)
                return new Success();

            return new Failed();
        }
        catch (Exception ex)
        {
            return new Failed(ex.Message);
        }
    }
}
