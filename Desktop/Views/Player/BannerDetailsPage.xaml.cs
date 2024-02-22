using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VideoDemos.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Core.Models.Bookmarks;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;


namespace VideoDemos.Views;

public partial class BannerDetailsPage : ContentPage
{
    public static Title currentTitle;
    public static int CurrentEpisode = 0;

    private static bool _isSeasonChooseing = false;
    private static bool _isEpisodeChooseing = false;

    private static IDispatcherTimer timer;

    public BannerDetailsPage()
    {
        InitializeComponent();
        string slug = BannerFactory.NavigatedBanner;

        BookmarksFactory.clickedEvent += BookmarksFactoryOnclickedEvent;
        string json = APIExecutor.ExecuteGet(Config.API_LINK + "/titles/" + slug);
        currentTitle =
            JsonConvert.DeserializeObject<Title>(json);
        currentTitle.Image.Uri = Config.IMAGE_LINK + currentTitle.Image.Uri;
        this.BindingContext = this;
        DetailsGrid.BindingContext = currentTitle;
        BackgroundImage.Source = currentTitle.Image.Uri;
        PreviewImage.Source = currentTitle.Image.Uri;
        RateingContainer.BindingContext = currentTitle;
        NavBarGrid.Children.Add(NavbarFactory.CreateNavBar(new AuthService()));
        DescriptionLabel.Text = $"Про що {currentTitle.Type} \"{currentTitle.Name}\":";

        if (currentTitle.Genres.Count > 0)
        {
            string request = Config.API_LINK + "/titles/by-genres?genres=";
            foreach (var gena in currentTitle.Genres)
            {
                request += $"{gena.Name}&genres=";
            }

            request = request.Substring(0, request.Length - 7);
            request += "count=6&page=0";
            string j = APIExecutor.ExecuteGet(request);
            List<Title> same = JsonConvert.DeserializeObject<List<Title>>(j);
            FilmsForYouLayout.Add(BannerFactory.CreateBannerCollection("Фільми для вас", same));
        }

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
            string res = APIExecutor.ExecutePost(Config.API_LINK + $"/register-view-record/{currentTitle.Id}",
                JsonConvert.SerializeObject(new DB_DuractionSendObject()
                {
                    WatchPercent = percentWatched,
                    WatchTime = DateTime.Now
                }));
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

    private void CreateCollectionClicked(object sender, EventArgs e)
    {
        ShowCreateNewFolderNotification();
        HideAddToFolderNotification();
    }

    private void HideAddToFolderNotification()
    {
        AddToFolder.Opacity = 0;
        AddToFolder.WidthRequest = 0;
        AddToFolder.HeightRequest = 0;
    }

    private void HideCreateNewFolderNotification()
    {
        CreateNewFolder.Opacity = 0;
        CreateNewFolder.WidthRequest = 0;
        CreateNewFolder.HeightRequest = 0;
    }

    private void ShowAddToFolderNotification()
    {
        AddToFolder.Opacity = 1;

        AddToFolder.HeightRequest = 623;
        AddToFolder.WidthRequest = 540;
    }

    private void ShowCreateNewFolderNotification()
    {
        CreateNewFolder.Opacity = 1;
        CreateNewFolder.HeightRequest = 623;
        CreateNewFolder.WidthRequest = 540;
    }

    private void CreateFolderConfirmClicked(object sender, EventArgs e)
    {
        if (FolderNameEntry.Text != null)
        {
            DBBookmarkModel @params = new DBBookmarkModel()
            {
                Availability = 0,
                Name = FolderNameEntry.Text
            };
            string result = APIExecutor.ExecutePost(Config.API_LINK + "/lists", JsonConvert.SerializeObject(@params));
            if (result != "")
            {
                HideCreateNewFolderNotification();
                ShowAddToFolderNotification();
            }
        }
    }

    private void CancelFolderCreationClicked(object sender, EventArgs e)
    {
        HideCreateNewFolderNotification();
        ShowAddToFolderNotification();
    }

    private void FavButtonClicked(object sender, EventArgs e)
    {
        APIExecutor.ExecutePost(Config.API_LINK + "/favorites/" + currentTitle.Id);
        ImageButton button = (ImageButton)sender;
        button.Source = "liked.png";
    }

    private void BookmarksButtonClicked(object sender, EventArgs e)
    {
        if (MainBookmarksContentScrollView.Content == null)
        {
            string data = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
            List<DBBanner> model = JsonConvert.DeserializeObject<List<DBBanner>>(data);
            VerticalStackLayout verticalStackLayout = new VerticalStackLayout();
            foreach (DBBanner bookmarkModel in model)
            {
                verticalStackLayout.Add(
                    BookmarksFactory.CreateSaveCollectionButton(bookmarkModel.Id, bookmarkModel.Name,
                        currentTitle.Image.Uri,
                        bookmarkModel.Availability == 1));
            }

            MainBookmarksContentScrollView.Content = verticalStackLayout;
        }

        if (AddToFolder.Opacity == 1)
        {
            HideAddToFolderNotification();
            return;
        }

        ShowAddToFolderNotification();
    }

    private void CommentsButtonClicked(object sender, EventArgs e)
    {
        if (CommentsEntry.Text != "")
        {
            string commentJson = APIExecutor.ExecutePost(Config.API_LINK + $"/comments/{currentTitle.Id}",
                JsonConvert.SerializeObject(new DB_CommentsSendobject()
                {
                    Content = CommentsEntry.Text,
                    CreationTime = DateTime.Now
                }));

            // DisplayAlert("Alert", commentJson, "OK");

            currentTitle.Comments.Add(JsonConvert.DeserializeObject<DB_Comment>(commentJson));
            CommentsContainer.Content = BannerDetailsFactory.CreateCommentsLayout(currentTitle.Comments);
        }
    }
}

class DB_CommentsSendobject
{
    public string Content { get; set; }
    public DateTime CreationTime { get; set; }
}

class DB_DuractionSendObject
{
    public int WatchPercent { get; set; }
    public DateTime WatchTime { get; set; }
}