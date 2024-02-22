using Domain.Interfaces.Hubs;
using Domain.Models.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;

namespace API.Hubs;

public class SessionHub : Hub<ISessionClient>
{
	private const string ANONYMOUS = "Anonymous";

	public SessionHub(ILogger<SessionHub> logger)
	{
		Logger = logger;
	}

	private ILogger<SessionHub> Logger { get; }

	private static List<Session> Sessions { get; } = new();

	public override async Task OnConnectedAsync()
	{
		Logger.LogInformation("User {ConnectionId} connected", Context.ConnectionId);
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		Logger.LogInformation("User {ConnectionId} disconnected", Context.ConnectionId);
	}

	public async Task PlaybackStarted(Guid id)
	{
		Logger.LogInformation("Received {message} to session {id}", nameof(PlaybackStarted), id);

		await Clients
			.OthersInGroup(id.ToString())
			.StartPlayback();
	}

	public async Task PlaybackStoped(Guid id)
	{
		Logger.LogInformation("Received {message} to session {id}", nameof(PlaybackStoped), id);

		await Clients
			.OthersInGroup(id.ToString())
			.StopPlayback();
	}

	public async Task ProgressChanged(Guid id, float value)
	{
		Logger.LogInformation("Received {message} to session {id} and value {value}", nameof(ProgressChanged), id, value);

		await Clients
			.OthersInGroup(id.ToString())
			.ChangeProgress(value);
	}

	public async Task SeriesChanged(Guid id, Guid value)
	{
		Logger.LogInformation("Received {message} to session {id} and value {value}", nameof(SeriesChanged), id, value);

		await Clients
			.OthersInGroup(id.ToString())
			.ChangeSeries(value);
	}

	public async Task SyncronizeConnection(string connection, Guid seriesId, float progress, bool isPlaying)
	{
		Logger.LogInformation("Syncronizing {connection} with ({seriesId}, {progress}, {isPlaying})", 
			connection, 
			seriesId, 
			progress, 
			isPlaying);

		await Clients.Client(connection).Syncronize(seriesId, progress, isPlaying);
	}

	public async Task JoinSession(Guid id, Guid titleId, Guid seriesId)
	{
		var session = Sessions.FirstOrDefault(x => x.Id == id);

		if (session is null)
			session = CreateSession(id, titleId, seriesId);

		if (session.TitleId != titleId)
			return;

		session.Connections.Add(Context.ConnectionId);

		await Groups.AddToGroupAsync(Context.ConnectionId, session.Id.ToString());

		await SendSessionMessage(id, $"{(Context.User?.Identity?.Name ?? ANONYMOUS)} connected.");

		var connection = session.Host;

		if (string.IsNullOrEmpty(connection) || connection.Equals(Context.ConnectionId))
			return;

		Logger.LogInformation("Requesting connection {connection} to get current state", connection);

		await Clients.Client(connection).RequestState(Context.ConnectionId);
	}

	public async Task LeaveSession(Guid id)
	{
		var session = Sessions.FirstOrDefault(x => x.Id == id);

		if (session is null)
			return;

		if (session.Connections.Contains(Context.ConnectionId) is false)
			return;

		session.Connections.Remove(Context.ConnectionId);

		await Groups.RemoveFromGroupAsync(Context.ConnectionId, session.Id.ToString());

		if (session.Connections.Any() is false) 
		{
			Sessions.Remove(session);
			return;
		}

		await SendSessionMessage(id, $"{(Context.User?.Identity?.Name ?? ANONYMOUS)} disconnected.");

		if (session.Host.Equals(Context.ConnectionId)) 
			session.Host = session.Connections.First();
	}

	public async Task SendSessionMessage(Guid id, string message)
	{
		await Clients.Group(id.ToString()).ReceiveMessage(message);
	}

	public Session CreateSession(Guid id, Guid titleId, Guid seriesId)
	{
		var session = new Session
		{
			Id = id,
			TitleId = titleId,
			Connections = new(),
			Host = Context.ConnectionId,
		};

		Sessions.Add(session);
		return session;
	}
}
