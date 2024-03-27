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

public partial class AnotherUserProfilePage : ContentPage
{
    public static Guid UserId { get; set; }
    private DBProfileModel _profileModel;
    private List<DBBanner> _bookmarks;
    private List<DBProfileModel> _readers;
    private List<DBProfileModel> _followers;
    private List<DB_Genre> _genres;

    private bool _isMainNotificationShown;

    public AnotherUserProfilePage()
    {
        InitializeComponent();
        if (UserId != null)
        {
            string pJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/profile");
            string bookmarksJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/lists");
            _profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);
            _bookmarks = JsonConvert.DeserializeObject<List<DBBanner>>(bookmarksJson);
            _followers =
                JsonConvert.DeserializeObject<List<DBProfileModel>>(
                    APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/followers"));
            _readers = JsonConvert.DeserializeObject<List<DBProfileModel>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/readers"));
            _genres = JsonConvert.DeserializeObject<List<DB_Genre>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/genres"));

            string favJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/favourites");
            string viewRecordsJson =
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/view-records");
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
    }


    private void ProfilePage_OnLoaded(object sender, EventArgs e)
    {
    }


    private void Fav_Clicked(object sender, EventArgs e)
    {
    }

    private void FollowClicked(object sender, EventArgs e)
    {
        APIExecutor.ExecutePost(Config.API_LINK + "/followers/" + UserId);
    }

    private void ProfilePage_OnAppearing(object? sender, EventArgs e)
    {
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/profile");
            string bookmarksJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/lists");
            _profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);
            _bookmarks = JsonConvert.DeserializeObject<List<DBBanner>>(bookmarksJson);
            _followers =
                JsonConvert.DeserializeObject<List<DBProfileModel>>(
                    APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/followers"));
            _readers = JsonConvert.DeserializeObject<List<DBProfileModel>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/readers"));
            _genres = JsonConvert.DeserializeObject<List<DB_Genre>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{UserId}/genres"));

            string favJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/favourites");
            string viewRecordsJson =
                APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/view-records");
            List<Title> fav = JsonConvert.DeserializeObject<List<Title>>(favJson);
            FavBannersLayout.Clear();
            FavBannersLayout.Add(BannerFactory.CreateFavBannerCollection("В обраному", fav));
            foreach (DBBanner bookmark in _bookmarks)
            {
                FavBannersLayout.Add(BannerFactory.CreateFavBannerCollection(bookmark.Name, bookmark.Titles));
            }
            GenreLayout.Clear();
            foreach (DB_Genre genre in _genres)
            {
                GenreLayout.Add(ProfileFactory.CreateGenre(genre.Name));
            }

            List<DB_ProggressBanner> banners = JsonConvert.DeserializeObject<List<DB_ProggressBanner>>(viewRecordsJson);
            RecentMoviesLayout.Clear();
            ProgressGrid.Clear();
        
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
}