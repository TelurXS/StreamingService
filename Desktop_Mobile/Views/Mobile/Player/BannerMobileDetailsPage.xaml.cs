using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Primitives;
using Metflix.Core;
using Metflix.Core.Mobile;
using Metflix.Core.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Core.Models.Bookmarks;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;
using Xflick.Views.Desktop.Player;
using Xflick.Views.Mobile.Player;

namespace VideoDemos.Views.Mobile.Player;

public partial class BannerMobileDetailsPage : ContentPage
{
    public static Title currentTitle;
    public static int CurrentEpisode = 0;

    private static bool _isSeasonChooseing = false;
    private static bool _isEpisodeChooseing = false;

    private static IDispatcherTimer timer;
    private static List<string> _dubbing;

    public BannerMobileDetailsPage()
    {
        InitializeComponent();
        string slug = BannerFactory.NavigatedBanner;
        BookmarksFactory.clickedEvent += BookmarksFactoryOnclickedEvent;
        
        string json = APIExecutor.ExecuteGet(Config.API_LINK + "/titles/" + slug);
        currentTitle =
            JsonConvert.DeserializeObject<Title>(json);
        string titleType = convertIntToType(currentTitle.Type);
        
        PositionSlider.CurrentMediaElem = mediaElement;
        playerControlsGrid.BindingContext = mediaElement;
        
        currentTitle.Image.Uri = Config.IMAGE_LINK + currentTitle.Image.Uri;
        this.BindingContext = this;
        DetailsGrid.BindingContext = currentTitle;
        BackgroundImage.Source = currentTitle.Image.Uri;
        PreviewImage.Source = currentTitle.Image.Uri;
        RateingContainer.BindingContext = currentTitle;
        
        FooterLayout.Add(FooterMobileFactory.CreateFooter(nameof(MainPage)));
        NavBarGrid.Add(FooterMobileFactory.CreateNavbar());        
        
        DescriptionLabel.Text = $"Про що {titleType} \"{currentTitle.Name}\":";
        TitleTypeLabel.Text = titleType;
        
        _dubbing = new List<string>();
        foreach (DB_Series currentTitleSeries in currentTitle.Series)
        {
            if (currentTitleSeries.Dubbing != null)
                if (!_dubbing.Contains(currentTitleSeries.Dubbing))
                {
                    _dubbing.Add(currentTitleSeries.Dubbing);
                    VozContainerLayout.Add(
                        BannerDetailsFactory.CreateVoz(new VozModel(currentTitleSeries.Dubbing, false)));
                }
        }
        
        if (currentTitle.Genres.Count > 0)
        {
            foreach (var gena in currentTitle.Genres)
            {
                GenresLabel.Text += $"  {gena.Name}  ";
            }
        }
        
        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);
        Shell.SetFlyoutBehavior(Application.Current.MainPage, FlyoutBehavior.Flyout);
        HideCreateSessionDialog();
        // TitleNameLabel.Text = currentTitle.Name;
        
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
        
        VideoSourceChanged();
    }
    
    private void ChangeVoz(string voz)
    {
        if (_dubbing != null)
        {
        }
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
        if (mediaElement.CurrentState == MediaElementState.Playing)
        {
            TimeSpan currentVideoPosition = mediaElement.Position;
            TimeSpan videoDuration = mediaElement.Duration;
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
    
    
    
    private void VideoSourceChanged()
    {
        if (!currentTitle.Series.IsNullOrEmpty() && !currentTitle.Series[CurrentEpisode].Uri.IsNullOrEmpty())
        {
            mediaElement.Source = (Config.IMAGE_LINK + currentTitle.Series[CurrentEpisode].Uri);
        }
    }
    
    private void OnEpisodesButtonClicked(object sender, EventArgs e)
    {
    }
    
    private void OnSubtitleButtonClicked(object sender, EventArgs e)
    {
    }
    
    private void OnPlaySpeedButtonClicked(object sender, EventArgs e)
    {
    }
    
    private void OnFullScreenButtonClicked(object sender, EventArgs e)
    {
    
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
                    if (currentTitle.Series[i].Dubbing == BannerDetailsFactory.SelectedVoz)
                    {
                        if (i > 5)
                            opacity -= 10;
                        EpisodesContainer.Children.Add(CreateAcrilicButton($"{currentTitle.Series[i].Name}",
                            opacity, i));
                    }
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
    
            currentTitle.Comments.Add(JsonConvert.DeserializeObject<DB_Comment>(commentJson));
            CommentsContainer.Content = BannerDetailsFactory.CreateCommentsLayout(currentTitle.Comments);
        }
    }
    
    private bool _isFullscreenToggle = false;
    
    public bool IsFullscreen
    {
        get { return _isFullscreenToggle; }
        set { _isFullscreenToggle = value; }
    }
    
    private void OnMinus10SecondsButtonClicked(object? sender, EventArgs e)
    {
        mediaElement.SeekTo(mediaElement.Position - new TimeSpan(0, 0, 0, 10));
    }
    
    private void OnPlayPauseButtonClicked(object? sender, EventArgs e)
    {
        if (mediaElement.CurrentState == MediaElementState.Playing)
        {
            mediaElement.Pause();
            PauseButton.Source = "play.png";
        }
        else if (mediaElement.CurrentState == MediaElementState.Paused)
        {
            mediaElement.Play();
            PauseButton.Source = "pause.png";
        }
    }
    
    private void OnPlus10SecondsButtonClicked(object? sender, EventArgs e)
    {
        mediaElement.SeekTo(mediaElement.Position + new TimeSpan(0, 0, 0, 10));
    }
    
    private void OnNextEpisodeClicked(object? sender, EventArgs e)
    {
    }
    
    private void PositionSlider_OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        TimeSpan newPosition = TimeSpan.FromSeconds(PositionSlider.Value);
        if (Math.Abs(newPosition.TotalSeconds - mediaElement.Position.TotalSeconds) /
            mediaElement.Duration.TotalSeconds > 0.01)
            mediaElement.SeekTo(newPosition);
    }
    
    private void CopySessionLink_OnClicked(object? sender, EventArgs e)
    {
        DisplayAlert("Success", "Link coppied", "ok");
        Clipboard.Default.SetTextAsync(ConnectedSessionPage.ConnectToSessionString);
    }
    
    private async void CreateSessionLink_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(BannerMobileConnectedSessionPage)}");
    }
    
    private void ShowCreateSessionDialog()
    {
        AddToConfirence.Opacity = 1;
        AddToConfirence.HeightRequest = 623;
        AddToConfirence.WidthRequest = 540;
    }
    
    private void HideCreateSessionDialog()
    {
        AddToConfirence.Opacity = 0;
        AddToConfirence.HeightRequest = 0;
        AddToConfirence.WidthRequest = 0;
    }
    
    private void ImageButton_OnClicked(object? sender, EventArgs e)
    {
        ConnectedSessionPage.SessionId = Guid.NewGuid().ToString();
        ConnectedSessionPage.ConnectToSessionString =
            Config.CREATE_SESSION_LINK + currentTitle.Slug + "/" + ConnectedSessionPage.SessionId;
        CopySessionLink.Text = ConnectedSessionPage.ConnectToSessionString;
        ShowCreateSessionDialog();
    }
    
    private void CloseSessionDialog_OnClicked(object? sender, EventArgs e)
    {
        HideCreateSessionDialog();
    }
}