using System.Net.Http.Headers;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Profile;
using VideoDemos.Views.Profile.Settings;

namespace VideoDemos.Views;

public partial class ProfilePage : ContentPage
{
    private DBProfileModel _profileModel;
    private List<DBBanner> _bookmarks;
    private List<DBProfileModel> _readers;
    private List<DBProfileModel> _followers;
    private List<DB_Genre> _genres;

    private bool _isMainNotificationShown;

    public ProfilePage()
    {
        InitializeComponent();
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        string bookmarksJson = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
        _profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);
        _bookmarks = JsonConvert.DeserializeObject<List<DBBanner>>(bookmarksJson);
        _followers =
            JsonConvert.DeserializeObject<List<DBProfileModel>>(APIExecutor.ExecuteGet(Config.API_LINK + "/followers"));
        _readers = JsonConvert.DeserializeObject<List<DBProfileModel>>(
            APIExecutor.ExecuteGet(Config.API_LINK + "/readers"));
        _genres = JsonConvert.DeserializeObject<List<DB_Genre>>(APIExecutor.ExecuteGet(Config.API_LINK + "/manage/genres"));
        
        string favJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/favourites");
        string viewRecordsJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/view-records");
        List<Title> fav = JsonConvert.DeserializeObject<List<Title>>(favJson);
        FavBannersLayout.Add(BannerFactory.CreateFavBannerCollection("В обраному", fav));
        foreach (DBBanner bookmark in _bookmarks)
        {
            FavBannersLayout.Add(BannerFactory.CreateFavBannerCollection(bookmark.Name, bookmark.Titles));
        }

        foreach (DB_Genre genre in _genres)
        {
            GenreLayout.Add(ProfileFactory.CreateGenre(genre.Name));
        }
        List<DB_ProggressBanner> banners = JsonConvert.DeserializeObject<List<DB_ProggressBanner>>(viewRecordsJson);
        if (banners.Count > 0)
        {
            RecentMoviesLayout.Add(ProfileFactory.CreateBannerCollection(banners));
            ProgressGrid.Add(ProfileFactory.CreateWatchProgress(banners[0].Progress));
            CurrentFilmImage.Source = Config.IMAGE_LINK + banners[0].Title.Image.Uri;
            CurrentFilmLabel.Text = banners[0].Title.Name;
        }
        
        ReadersAmountLabel.Text = _readers.Count.ToString();
        FollowersAmountLabel.Text = _followers.Count.ToString();
        FavLabel.Text = fav.Count.ToString();
        
        
        NicknameLabel.Text = _profileModel.Name;
        NameLabel.Text = _profileModel.FirstName + " " + _profileModel.SecondName;
        ProfileImage.Source = Config.IMAGE_LINK + _profileModel.ProfileImage;
    }


    private void ProfilePage_OnLoaded(object sender, EventArgs e)
    {
    }

    private async void SettingsButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }

    private void Followers_click(object sender, EventArgs e)
    {
        if (_isMainNotificationShown)
        {
            HideMainNotification();
            return;
        }

        ShowMainNotification();
        MainContentScrollView.Content = GenerateFollowersScrollView();
        NotificationLabel.Text = "Відстежуються";
    }

    private VerticalStackLayout GenerateFollowersScrollView()
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();

        foreach (var follower in _followers)
        {
            HorizontalStackLayout readerLayout = new HorizontalStackLayout()
            {
                Margin = new Thickness(0, 0, 0, 15)
            };
            readerLayout.Add(new Image()
            {
                Source = Config.IMAGE_LINK + follower.ProfileImage,
                WidthRequest = 70,
                HeightRequest = 70,
                Margin = 15,
                Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 70, 70))
            });
            VerticalStackLayout detailsLayout = new VerticalStackLayout()
            {
                Margin = new Thickness(0, 25)
            };
            detailsLayout.Add(new Label()
            {
                Text = follower.Name,
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 5)
            });
            detailsLayout.Add(new Label()
            {
                Text = follower.FirstName + " " + follower.SecondName,
                FontSize = 16,
                Opacity = 0.5
            });
            readerLayout.Add(detailsLayout);

            mainLayout.Add(readerLayout);
        }

        return mainLayout;
    }


    private void Readers_Clicked(object sender, EventArgs e)
    {
        if (_isMainNotificationShown)
        {
            HideMainNotification();
            return;
        }

        ShowMainNotification();
        MainContentScrollView.Content = GenerateReadersScrollView();
        NotificationLabel.Text = "Читачі";
    }

    private VerticalStackLayout GenerateReadersScrollView()
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();

        foreach (var follower in _readers)
        {
            HorizontalStackLayout readerLayout = new HorizontalStackLayout()
            {
                Margin = new Thickness(0, 0, 0, 15)
            };
            readerLayout.Add(new Image()
            {
                Source = Config.IMAGE_LINK + follower.ProfileImage,
                WidthRequest = 70,
                HeightRequest = 70,
                Margin = 15,
                Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 70, 70))
            });
            VerticalStackLayout detailsLayout = new VerticalStackLayout()
            {
                Margin = new Thickness(0, 25)
            };
            detailsLayout.Add(new Label()
            {
                Text = follower.Name,
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 5)
            });
            detailsLayout.Add(new Label()
            {
                Text = follower.FirstName + " " + follower.SecondName,
                FontSize = 16,
                Opacity = 0.5
            });
            readerLayout.Add(detailsLayout);
            mainLayout.Add(readerLayout);
        }

        return mainLayout;
    }


    private void Fav_Clicked(object sender, EventArgs e)
    {
    }

    private void ShowMainNotification()
    {
        MainNotfication.Opacity = 1;
        MainNotfication.WidthRequest = 540;
        MainNotfication.HeightRequest = 618;
        _isMainNotificationShown = true;
    }

    private void HideMainNotification()
    {
        MainNotfication.Opacity = 0;
        MainNotfication.WidthRequest = 0;
        MainNotfication.HeightRequest = 0;
        MainContentScrollView.Content = null;
        _isMainNotificationShown = false;
    }

    private async void EditNameClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ProfileEditPage)}");
    }

    private async void ChooseAvatarClicked(object sender, EventArgs e)
    {
        var customFileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "image/jpeg", "image/png", "image/gif" } },
                { DevicePlatform.WinUI, new[] { ".jpg", ".png", ".gif" } },
                { DevicePlatform.Tizen, new[] { "image/jpeg", "image/png", "image/gif" } },
            });

        PickOptions options = new()
        {
            PickerTitle = "Please select a image file",
            FileTypes = customFileType,
        };
        await PickAndShow(options);
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        FileResult result = await FilePicker.Default.PickAsync(options);
        if (result != null)
        {
            if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
            {
                using var stream = await result.OpenReadAsync();
                var image = ImageSource.FromStream(() => stream);
                string serverResult = "";
                AuthService service = new AuthService();

                // Create the HttpClient
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", service.GetAccessToken());

                // Create the multipart form data content
                using var formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(stream), "file", result.FileName);

                // Send the request
                try
                {
                    var response = await client.PostAsync(Config.API_LINK + "/manage/image/", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        serverResult = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode + " - " + response.ReasonPhrase);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        ProfileImage.Source = result.FullPath;
        return result;
    }

    private void ProfilePage_OnAppearing(object? sender, EventArgs e)
    {
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        string bookmarksJson = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
        _profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);
        _bookmarks = JsonConvert.DeserializeObject<List<DBBanner>>(bookmarksJson);
        _followers =
            JsonConvert.DeserializeObject<List<DBProfileModel>>(APIExecutor.ExecuteGet(Config.API_LINK + "/followers"));
        _readers = JsonConvert.DeserializeObject<List<DBProfileModel>>(
            APIExecutor.ExecuteGet(Config.API_LINK + "/readers"));
        _genres = JsonConvert.DeserializeObject<List<DB_Genre>>(APIExecutor.ExecuteGet(Config.API_LINK + "/manage/genres"));
        GenreLayout.Clear();
        foreach (DB_Genre genre in _genres)
        {
            GenreLayout.Add(ProfileFactory.CreateGenre(genre.Name));
        }
        
        NicknameLabel.Text = _profileModel.Name;
        NameLabel.Text = _profileModel.FirstName + " " + _profileModel.SecondName;
        ProfileImage.Source = Config.IMAGE_LINK + _profileModel.ProfileImage;
    }

    private async void AddGenresClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(GenreChangePage)}");
    }
}