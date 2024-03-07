using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Mobile;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Mobile.Profile;

public partial class ProfileMobilePage : ContentPage
{
    private DBProfileModel _profileModel;
    private List<DBBanner> _bookmarks;
    private List<DBProfileModel> _readers;
    private List<DBProfileModel> _followers;

    private bool _isMainNotificationShown;
    public ProfileMobilePage()
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

        string favJson = APIExecutor.ExecuteGet(Config.API_LINK + $"/users/{_profileModel.Id}/favourites");

        foldersLayout.Add(BannerFactory.CreateFavMobileBannerCollection("В обраному",
            JsonConvert.DeserializeObject<List<Title>>(favJson)));

        foreach (DBBanner bookmark in _bookmarks)
        {
            foldersLayout.Add(BannerFactory.CreateFavMobileBannerCollection(bookmark.Name, bookmark.Titles));
        }

        ReadersAmountLabel.Text = _readers.Count.ToString();
        FollowersAmountLabel.Text = _followers.Count.ToString();

        NicknameLabel.Text = _profileModel.Name;
        NameLabel.Text = _profileModel.FirstName + " " + _profileModel.SecondName;
        ProfileImage.Source = Config.IMAGE_LINK + _profileModel.ProfileImage;
        FooterLayout.Add(FooterMobileFactory.CreateFooter(nameof(ProfileMobilePage)));
    }

    private void ChooseAvatarClicked(object sender, EventArgs e)
    {
    }

    private void EditNameClicked(object sender, EventArgs e)
    {
    }

    private void Fav_Clicked(object sender, EventArgs e)
    {
    }

    private void Followers_click(object sender, EventArgs e)
    {
    }

    private void Readers_Clicked(object sender, EventArgs e)
    {
    }
}