using Metflix.Core.Models;
using VideoDemos.Controls;

namespace VideoDemos.Views;

[QueryProperty(nameof(Url), "Url")]
[QueryProperty(nameof(Name), "Name")]
[QueryProperty(nameof(VideoId), "Id")]
public partial class VideoPlayerPage : ContentPage
{
    private string _url;
    private string _name;
    private int _id;
    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            VideoSourceChanged();
        }
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int VideoId
    {
        get => _id;
        set => _id = value;
    }


#if WINDOWS
    private Microsoft.UI.Windowing.AppWindow GetAppWindow(MauiWinUIWindow window)
    {
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
        return appWindow;
    }
#endif
    private void VideoSourceChanged()
    {
        video.Source = VideoSource.FromUri(Url);
        video.AutoPlay = true;
    }

    public VideoPlayerPage()
    {
        InitializeComponent();
        BindingContext = this;
        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);
        Shell.SetFlyoutBehavior(Application.Current.MainPage, FlyoutBehavior.Flyout);
        TitleNameLabel.Text = Name;
    }

    void OnPlayPauseButtonClicked(object sender, EventArgs args)
    {
        if (video.Status == VideoStatus.Playing)
        {
            video.Pause();
        }
        else if (video.Status == VideoStatus.Paused)
        {
            video.Play();
        }
    }

    void OnContentPageUnloaded(object sender, EventArgs e)
    {
        video.Handler?.DisconnectHandler();
    }

    private void OnMinus10SecondsButtonClicked(object sender, EventArgs e)
    {
        video.Position = video.Position.Add(new TimeSpan(0, 0, 0, -10));
    }

    private void OnPlus10SecondsButtonClicked(object sender, EventArgs e)
    {
        video.Position = video.Position.Add(new TimeSpan(0, 0, 0, 10));
    }

    private void OnNextEpisodeClicked(object sender, EventArgs e)
    {
        video.Stop();
        // video.Source = VideoSource.FromUri(_videoModel.NextEpisode.Url);
        video.Play();
    }

    private void OnEpisodesButtonClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSubtitleButtonClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPlaySpeedButtonClicked(object sender, EventArgs e)
    {
    }

    private void OnFullScreenButtonClicked(object sender, EventArgs e)
    {
#if WINDOWS
        var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;

        var appWindow = GetAppWindow(window);

        switch (appWindow.Presenter)
        {
            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                if (overlappedPresenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
                {
                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                    overlappedPresenter.Restore();
                }
                else
                {
                    overlappedPresenter.SetBorderAndTitleBar(false, false);
                    overlappedPresenter.Maximize();
                }

                break;
        }
#endif
    }
}