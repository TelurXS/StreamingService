using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Mobile;
using Metflix.Core.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VideoDemos.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Core.Models.Bookmarks;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;

namespace VideoDemos.Views.Mobile.Player;

public partial class BannerMobileDetailsPage : ContentPage
{
    public static Title currentTitle;
    public static int CurrentEpisode = 0;

    private static bool _isSeasonChooseing = false;
    private static bool _isEpisodeChooseing = false;

    private static IDispatcherTimer timer;
    public BannerMobileDetailsPage()
    {
        InitializeComponent();
           string slug = BannerFactory.NavigatedBanner;
        BookmarksFactory.clickedEvent += BookmarksFactoryOnclickedEvent;
        string json = APIExecutor.ExecuteGet(Config.API_LINK + "/titles/" + slug);
        currentTitle =
            JsonConvert.DeserializeObject<Title>(json);
        string titleType = convertIntToType(currentTitle.Type);

        currentTitle.Image.Uri = Config.IMAGE_LINK + currentTitle.Image.Uri;
        this.BindingContext = this;
        DetailsGrid.BindingContext = currentTitle;
        BackgroundImage.Source = currentTitle.Image.Uri;
        PreviewImage.Source = currentTitle.Image.Uri;
        RateingContainer.BindingContext = currentTitle;
        NavBarGrid.Children.Add(FooterMobileFactory.CreateNavbar());
        DescriptionLabel.Text = $"Про що {titleType} \"{currentTitle.Name}\":";
        TitleTypeLabel.Text = titleType;

        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);
        Shell.SetFlyoutBehavior(Application.Current.MainPage, FlyoutBehavior.Flyout);
        TitleNameLabel.Text = currentTitle.Name;

        if (currentTitle.Series.Count > 1)
        {
        }

        CommentsContainer.Content = BannerDetailsFactory.CreateCommentsLayout(currentTitle.Comments);
        foreach (var screen in currentTitle.Screenshots)

        {
            ScreenshotsContainer.Add(BannerDetailsFactory.CreateScreenshot(screen));
        }


        timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(5);
        timer.Tick += (s, e) => SaveProgress();
        timer.Start();

        video.Stop();
        VideoSourceChanged();
    }
     private string convertIntToType(int typeNum)
    {
        switch (typeNum)
        {
            case 1: return "фiльм";
            case 0: return "серiал";
        }

        return "exc";
    }

    private void SaveProgress()
    {
        if (video.Status == VideoStatus.Playing)
        {
            TimeSpan currentVideoPosition = video.Position;
            TimeSpan videoDuration = video.Duration;
            int percentWatched;
            if (videoDuration.Seconds / 100 == 0)
            {
                percentWatched = 0;
            }
            else
            {
                percentWatched = currentVideoPosition.Seconds / (videoDuration.Seconds / 100);
            }

            if (currentTitle.Series != null)
            {
                APIExecutor.ExecutePost(
                    Config.API_LINK + $"/register-view-record/{currentTitle.Series[CurrentEpisode].Id}",
                    JsonConvert.SerializeObject(new DB_DuractionSendObject()
                    {
                        WatchPercent = percentWatched,
                        WatchTime = DateTime.Now
                    }));
            }
        }
    }

    private void BookmarksFactoryOnclickedEvent(object sender, EventArgs e)
    {
        Button button = ((Button)sender);
        APIExecutor.ExecutePost(Config.API_LINK + "/lists/" + button.Text + "/" + currentTitle.Id);
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
        if (!currentTitle.Series.IsNullOrEmpty() && !currentTitle.Series[CurrentEpisode].Uri.IsNullOrEmpty())
        {
            video.Source = VideoSource.FromUri(Config.IMAGE_LINK + currentTitle.Series[CurrentEpisode].Uri);
            video.AutoPlay = false;
        }
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
        button.Clicked += (sender, args) =>
        {
            CurrentEpisode = episodeNumber;
            VideoSourceChanged();
        };
        button_view.Add(button);

        acrylicView.Content = button_view;

        return acrylicView;
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
    private void CommentsButtonClicked(object sender, EventArgs e)
    {
        if (CommentsEntry.Text != "")
        {
            string commentJson = APIExecutor.ExecutePost(Config.API_LINK + $"/comments/{currentTitle.Id}",
                JsonConvert.SerializeObject(new DB_CommentsSendobject()
                {
                    Content = CommentsEntry.Text,
                    CreationTime = DateTime.Now - TimeSpan.FromSeconds(20)
                }));

            DisplayAlert("Alert", commentJson, "OK");
            currentTitle.Comments.Add(JsonConvert.DeserializeObject<DB_Comment>(commentJson));
            CommentsContainer.Content = BannerDetailsFactory.CreateCommentsLayout(currentTitle.Comments);
        }
    }
}