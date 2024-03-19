using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Core.Steaming;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;

namespace Xflick.Views.Desktop.Player;

public partial class ConnectedSessionPage : ContentPage
{
    public static Title currentTitle;
    public static int CurrentEpisode = 0;

    private static bool _isSeasonChooseing = false;
    private static bool _isEpisodeChooseing = false;

    private static IDispatcherTimer timer;

    public static string ConnectToSessionString { get; set; }
    private HubConnection? Connection { get; set; } = default;
    private bool Loaded { get; set; } = false;
    private bool Joined { get; set; } = false;
    private List<string> Messages { get; set; } = new();
    public static string SessionId { get; set; } = string.Empty;
    public DB_Series CurrentSeries { get; set; }
    public Title Title { get; set; }

    private string convertIntToType(int typeNum)
    {
        switch (typeNum)
        {
            case 1: return "фiльм";
            case 0: return "серiал";
        }

        return "exc";
    }

    public async void Connect()
    {
        Connection = new HubConnectionBuilder()
            .WithUrl($"{Config.HUBS_LINK}")
            .Build();

        Connection.On<string>(SessionMessages.RECEIVE_MESSAGE, OnRecieveMessage);

        Connection.On(SessionMessages.START_PLAYBACK, OnStartPlayback);

        Connection.On(SessionMessages.STOP_PLAYBACK, OnStopPlayback);

        Connection.On<float>(SessionMessages.CHANGE_PROGRESS, OnChangeProgress);

        Connection.On<Guid>(SessionMessages.CHANGE_SERIES, OnChangeSeries);

        Connection.On<Guid, float, bool>(SessionMessages.SYNCRONIZE, OnSyncronize);

        Connection.On<string>(SessionMessages.REQUST_STATE, OnRequestState);
        await Connection.StartAsync();
        Loaded = true;
    }

    public ConnectedSessionPage()
    {
        InitializeComponent();

        string slug = BannerFactory.NavigatedBanner;


        string json = APIExecutor.ExecuteGet(Config.API_LINK + "/titles/" + slug);
        currentTitle =
            JsonConvert.DeserializeObject<Title>(json);
        string titleType = convertIntToType(currentTitle.Type);

        PositionSlider.CurrentMediaElem = mediaElement;
        playerControlsGrid.BindingContext = mediaElement;

        TitleNameLabel.Text = currentTitle.Name;
        currentTitle.Image.Uri = Config.IMAGE_LINK + currentTitle.Image.Uri;
        this.BindingContext = this;
        DetailsGrid.BindingContext = currentTitle;
        BackgroundImage.Source = currentTitle.Image.Uri;
        PreviewImage.Source = currentTitle.Image.Uri;
        NavBarGrid.Children.Add(NavbarFactory.CreateNavBar(new AuthService()));
        DescriptionLabel.Text = $"Про що {titleType} \"{currentTitle.Name}\":";
        TitleTypeLabel.Text = titleType;
        if (currentTitle.Genres.Count > 0)
        {
            foreach (var gena in currentTitle.Genres)
            {
                GenresLabel.Text += $"  {gena.Name}  ";
            }
        }


        VideoSourceChanged();
        Connect();
    }

    private void SeasonButton_OnClicked(object sender, EventArgs e)
    {
        // if (FilmsDetails.SeasonCount > 1)
        // {
        //     if (!_isSeasonChooseing)
        //     {
        //         int opacity = 100;
        //         for (int i = 0; i <= FilmsDetails.SeasonCount; i++)
        //         {
        //             if (i > 5) opacity -= 10;
        //             SeasonContainer.Children.Add(CreateAcrilicButton("Сезон " + (i + 1), opacity, i));
        //         }
        //
        //         _isSeasonChooseing = true;
        //     }
        //     else
        //     {
        //         SeasonContainer.Children.Clear();
        //         _isSeasonChooseing = false;
        //     }
        // }
    }

    private void EpisodeButton_OnClicked(object sender, EventArgs e)
    {
        if (currentTitle.Series.Count > 1)
        {
            if (!_isEpisodeChooseing)
            {
                int opacity = 100;
                for (int i = 0; i < currentTitle.Series.Count; i++)
                {
                    if (i > 5) opacity -= 10;
                    EpisodesContainer.Children.Add(CreateAcrilicButton("Серiя " + (i + 1), opacity, i));
                }

                _isEpisodeChooseing = true;
            }
            else
            {
                EpisodesContainer.Children.Clear();
                _isEpisodeChooseing = false;
            }
        }
    }

    private AcrylicView CreateAcrilicButton(string text, int opacity, int episodeNumber)
    {
        AcrylicView acrylicView = new AcrylicView
        {
            EffectStyle = EffectStyle.ExtraDark,
            WidthRequest = 190,
            HeightRequest = 50,
            Margin = new Thickness(0, 10, 0, 0),
            Opacity = opacity / 100f,
            CornerRadius = 20,
            BorderThickness = 0,
            Padding = 0,
            TintOpacity = 0.4
        };
        HorizontalStackLayout button_view = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Padding = 0
        };
        Button button = new Button
        {
            FontSize = 18,
            CornerRadius = 20,
            WidthRequest = 190,
            HeightRequest = 50,
            Background = new SolidColorBrush(new Color(0, 0, 0, 0.8f)),
            BorderWidth = 0,
            Text = text
        };
        button.Clicked += async (sender, args) =>
        {
            CurrentEpisode = episodeNumber;
            VideoSourceChanged();

            await Connection!.SendAsync(SessionMessages.SERIES_CHANGED, SessionId,
                currentTitle.Series[CurrentEpisode].Id);
        };
        button_view.Add(button);

        acrylicView.Content = button_view;

        return acrylicView;
    }

    private void VideoSourceChanged()
    {
        if (!currentTitle.Series.IsNullOrEmpty() && !currentTitle.Series[CurrentEpisode].Uri.IsNullOrEmpty())
        {
            mediaElement.Source = (Config.IMAGE_LINK + currentTitle.Series[CurrentEpisode].Uri);
        }
    }

    private void OnEpisodesButtonClicked(object sender, EventArgs e)
    {
        SendSeekToToServer(mediaElement.Position, mediaElement.Duration);
    }

    private void OnSubtitleButtonClicked(object sender, EventArgs e)
    {
    }

    private void OnPlaySpeedButtonClicked(object sender, EventArgs e)
    {
    }

    private void OnFullScreenButtonClicked(object sender, EventArgs e)
    {
#if WINDOWS
        var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;

        // var appWindow = GetAppWindow(window);

        // switch (appWindow.Presenter)
        // {
        //     case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
        //         if (overlappedPresenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
        //         {
        //             overlappedPresenter.SetBorderAndTitleBar(true, true);
        //             overlappedPresenter.Restore();
        //         }
        //         else
        //         {
        //             overlappedPresenter.SetBorderAndTitleBar(false, false);
        //             overlappedPresenter.Maximize();
        //             
        //         }
        //
        //         break;
        // }
#endif
    }

    private async void OnMinus10SecondsButtonClicked(object? sender, EventArgs e)
    {
        await mediaElement.SeekTo(mediaElement.Position - new TimeSpan(0, 0, 0, 10));
        await Connection!.SendAsync(SessionMessages.PROGRESS_CHANGED, SessionId, mediaElement.Position.TotalSeconds);
    }

    private async void OnPlayPauseButtonClicked(object? sender, EventArgs e)
    {
        if (mediaElement.CurrentState == MediaElementState.Playing)
        {
            mediaElement.Pause();
            PauseButton.Source = "play.png";
            await Connection!.SendAsync(SessionMessages.PLAYBACK_STOPED, SessionId);
        }
        else if (mediaElement.CurrentState == MediaElementState.Paused)
        {
            mediaElement.Play();
            PauseButton.Source = "pause.png";
            await Connection!.SendAsync(SessionMessages.PLAYBACK_STARTED, SessionId);
        }
    }

    private async void OnPlus10SecondsButtonClicked(object? sender, EventArgs e)
    {
        await mediaElement.SeekTo(mediaElement.Position + new TimeSpan(0, 0, 0, 10));
        SendSeekToToServer(mediaElement.Position, mediaElement.Duration);
    }

    private async void SendSeekToToServer(TimeSpan pos, TimeSpan dur)
    {
        double procent = (pos.TotalSeconds / dur.TotalSeconds);
        await Connection!.SendAsync(SessionMessages.PROGRESS_CHANGED, SessionId, procent);
    }

    private void OnNextEpisodeClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void ConnectedSessionPage_OnLoaded(object? sender, EventArgs e)
    {
        await OnJoin(currentTitle);
        Messages.Clear();
        ChatListView.Children.Clear();
    }


    public async Task OnJoin(Title title)
    {
        Joined = true;
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);

        await Connection!.SendAsync(
            SessionMessages.JOIN_SESSION,
            SessionId,
            profileModel.Name,
            title.Id,
            title.Series.FirstOrDefault()?.Id);
    }


    private async Task OnStartPlayback()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            mediaElement.Play();
            PauseButton.Source = "play.png";
        });
    }

    private async Task OnStopPlayback()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            mediaElement.Pause();
            PauseButton.Source = "pause.png";
        });
    }

    private async Task OnChangeProgress(float value)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            mediaElement.SeekTo(TimeSpan.FromSeconds(value * mediaElement.Duration.TotalSeconds));
            SendSeekToToServer(mediaElement.Position, mediaElement.Duration);
        });
    }

    private async Task OnChangeSeries(Guid value)
    {
        foreach (var series in Title.Series)
        {
            if (series.Id == value)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    mediaElement.Source = (Config.IMAGE_LINK + series.Uri);
                    CurrentSeries = series;
                });
                return;
            }
        }
    }

    private async Task OnSyncronize(Guid seriesId, float progress, bool isPlaying)
    {
        foreach (var series in Title.Series)
        {
            if (series.Id == seriesId)
            {
                if (series.Id == CurrentSeries.Id) break;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    mediaElement.Source = (Config.IMAGE_LINK + series.Uri);
                    CurrentSeries = series;
                });
                break;
            }
        }

        while (mediaElement.IsLoaded is false)
            await Task.Delay(100);
        MainThread.BeginInvokeOnMainThread(() => { mediaElement.SeekTo(TimeSpan.FromSeconds(progress)); });
        if (isPlaying)
            MainThread.BeginInvokeOnMainThread(() => { mediaElement.Pause(); });
    }

    private async Task OnRequestState(string connection)
    {
        await Connection!.SendAsync(
            SessionMessages.SYNCRONIZE_CONNECTION,
            connection,
            CurrentSeries.Id,
            mediaElement.Position.TotalSeconds,
            mediaElement.CurrentState == MediaElementState.Playing
        );
    }


    public async ValueTask DisposeAsync()
    {
        if (Connection is null)
            return;
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);

        await Connection.SendAsync(SessionMessages.LEAVE_SESSION, SessionId, profileModel.Name);

        await Connection.DisposeAsync();
    }

    private async void ConnectedSessionPage_OnDisappearing(object? sender, EventArgs e)
    {
        await DisposeAsync();
    }

    private HorizontalStackLayout CreateChatMessage(string mes)
    {
        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout();
        horizontalStackLayout.Add(new Label()
        {
            Text = mes
        });
        return horizontalStackLayout;
    }

    private async void SendMessageToChat(object? sender, EventArgs e)
    {
        string mes = ChatEntry.Text;
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);

        await Connection.SendAsync(SessionMessages.SEND_SESSION_MESSAGE, SessionId,
            $"{profileModel.Name}: {mes}");
    }

    private async Task OnRecieveMessage(string message)
    {
        Messages.Add(message);
        MainThread.BeginInvokeOnMainThread(() => { ChatListView.Children.Add(CreateChatMessage(message)); });
    }
}