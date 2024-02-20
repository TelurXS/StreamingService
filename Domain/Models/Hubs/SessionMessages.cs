using Domain.Interfaces.Hubs;

namespace Domain.Models.Hubs;

public class SessionMessages
{
	public const string RECEIVE_MESSAGE = nameof(ISessionClient.ReceiveMessage);

	public const string START_PLAYBACK = nameof(ISessionClient.StartPlayback);

	public const string STOP_PLAYBACK = nameof(ISessionClient.StopPlayback);

	public const string CHANGE_PROGRESS = nameof(ISessionClient.ChangeProgress);

	public const string CHANGE_SERIES = nameof(ISessionClient.ChangeSeries);

	public const string PLAYBACK_STARTED = "PlaybackStarted";

	public const string PLAYBACK_STOPED = "PlaybackStoped";

	public const string PROGRESS_CHANGED = "ProgressChanged";

	public const string SERIES_CHANGED = "SeriesChanged";

	public const string JOIN_SESSION = "JoinSession";

	public const string LEAVE_SESSION = "LeaveSession";
}