using Domain.Interfaces.Hubs;
using Domain.Models.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;

namespace API.Hubs;

public class SessionHub : Hub<ISessionClient>
{
    public SessionHub(ILogger<SessionHub> logger)
    {
		Logger = logger;
		Sessions = new();
	}

	private ILogger<SessionHub> Logger { get; }
	private List<Session> Sessions { get; }

	public override async Task OnConnectedAsync()
	{
		await Clients
			.All
			.ReceiveMessage($"User {Context.User?.Identity?.Name} {{{Context.ConnectionId}}} connected");

		Logger.LogInformation("User {ConnectionId} connected", Context.ConnectionId);
	}


	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients
			.All
			.ReceiveMessage($"User {Context.User?.Identity?.Name} {{{Context.ConnectionId}}} disconnected");

		Logger.LogInformation("User {ConnectionId} disconnected", Context.ConnectionId);
	}

	public async Task PlaybackStarted()
	{
		await Clients.All.StartPlayback();
	}

    public async Task PlaybackStoped()
    {
        await Clients.All.StopPlayback();
    }

    public async Task ProgressChanged(float value)
    {
        await Clients.All.ChangeProgress(value);
    }

	public async Task SeriesChanged(Guid value)
	{
		await Clients.All.ChangeSeries(value);
	}

	public async Task JoinSession(Guid id, Guid titleId, Guid seriesId)
	{
		var session = Sessions.FirstOrDefault(x => x.Id == id);

		if (session is null)
		{
			session = await CreateSession(titleId, seriesId);
		}

		session.Connections.Add(Context.ConnectionId);

		await Groups.AddToGroupAsync(Context.ConnectionId, session.Id.ToString());
	}

	public async Task LeaveSession(Guid id)
	{
		var session = Sessions.FirstOrDefault(x => x.Id == id);

		if (session is null)
			return;

		session.Connections.Remove(Context.ConnectionId);

		if (session.Connections.Any() is false)
			Sessions.Remove(session);

		await Groups.RemoveFromGroupAsync(Context.ConnectionId, session.Id.ToString());
	}

	public async Task<Session> CreateSession(Guid titleId, Guid seriesId)
	{
		var session = new Session
		{
			Id = Guid.NewGuid(),
			TitleId = titleId,
			State = new SessionState
			{
				SeriesId = seriesId,
				Progress = 0,
				IsPlaying = false,
			},
		};

		Sessions.Add(session);
		return session;
	}
}
