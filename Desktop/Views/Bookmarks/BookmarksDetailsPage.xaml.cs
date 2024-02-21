﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Bookmarks;

[QueryProperty(nameof(ListId), "listId")]
public partial class BookmarksDetailsPage : ContentPage
{
    private int _bookmarkId;
    private Guid listId;

    public Guid ListId
    {
        get => listId;
        set
        {
            listId = value;
            OnPropertyChanged();
        }
    }

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
            return;
        }

        _banner.Availability = 0;
        LockedImage.Source = "unlock.png";
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
        string result = APIExecutor.ExecuteGet(Config.API_LINK + $"/lists/{ListId}");
        _banner = JsonConvert.DeserializeObject<DBBanner>(result);
        CollectionNameLabel.Text = _banner.Name;
        PrivacySwitch.IsToggled = _banner.Availability == 1;
        foreach (var banner in _banner.Titles)
        {
            banner.Image.Uri = Config.IMAGE_LINK + banner.Image.Uri;
            MainContainer.Add(BookmarksFactory.CreateDetailsBanner(banner));
        }

        PrivacySwitch.Toggled += PrivacyChanged;
    }
}