using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Bookmarks;

public partial class BookmarksDetailsPage : ContentPage
{
    private int _bookmarkId;
    public static Guid CurrentListId;
    

    private DBBanner _banner;
    private string name = "Комедія";

    public BookmarksDetailsPage()
    {
        InitializeComponent();
    }

    private void PrivacyChanged(object sender, ToggledEventArgs e)
    {
        Switch @switch = (Switch)sender;

        if (@switch.IsToggled)
        {
            _banner.Availability = 1;
            LockedImage.Source = "lock.png";
            LockImageButton.Source = "lock.png";

            return;
        }

        _banner.Availability = 0;
        LockedImage.Source = "unlock.png";
        LockImageButton.Source = "unlock.png";
    }

    private void LockOpenMenuClicked(object sender, EventArgs e)
    {
        if (ChangePrivacyMenu.Opacity == 0)
        {
            ChangePrivacyMenu.Opacity = 1;
            return;
        }

        ChangePrivacyMenu.Opacity = 0;
    }


    private void BookmarksDetailsPage_OnLoaded(object sender, EventArgs e)
    {
        // if(ListId == null) BannerFactory.NavigatedBanner 
        string result = APIExecutor.ExecuteGet(Config.API_LINK + $"/lists/{CurrentListId}");
        _banner = JsonConvert.DeserializeObject<DBBanner>(result);
        CollectionNameLabel.Text = _banner.Name;
        PrivacySwitch.IsToggled = _banner.Availability == 1;
        foreach (var banner in _banner.Titles)
        {
            banner.Image.Uri = Config.IMAGE_LINK + banner.Image.Uri;
            MainContainer.Add(BookmarksFactory.CreateDetailsBanner(banner));
        }

        _banner.Availability = 0;
        LockedImage.Source = "unlock.png";
        LockImageButton.Source = "unlock.png";

        if (_banner.Availability == 1)
        {
            LockedImage.Source = "lock.png";
            LockImageButton.Source = "lock.png";
        }

        PrivacySwitch.Toggled += PrivacyChanged;
    }
}