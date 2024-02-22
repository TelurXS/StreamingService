namespace Domain.Interfaces.Hubs;

public interface ISessionClient
{
	Task ReceiveMessage(string message);

	Task StartPlayback();

	Task StopPlayback();

	Task ChangeProgress(float progress);

	Task ChangeSeries(Guid seriesId);

	Task Syncronize(Guid seriesId, float progress, bool isPlaying);

	Task RequestState(string connection);
}
