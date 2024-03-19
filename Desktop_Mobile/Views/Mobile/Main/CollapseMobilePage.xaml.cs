using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Mobile;
using Microsoft.Maui.Controls;

namespace VideoDemos.Views.Mobile.Main;

public partial class CollapseMobilePage : ContentPage
{
    public CollapseMobilePage()
    {
        InitializeComponent();
        switch (FooterMobileFactory.CurrentPage)
        {
            case nameof(MainMobilePage):
                MainButton.Opacity = 1;
                break;
            case nameof(MainFilmsPage):
                FilmsButton.Opacity = 1;
                break;
            case nameof(MainTVPage):
                TVButton.Opacity = 1;
                break;
            case nameof(MainNewAndPopularPage):
                NewsButton.Opacity = 1;
                break;
            case nameof(MainMyListPage):
                MyListButton.Opacity = 1;
                break;
            case nameof(MainWatchViaLanguagesPage):
                WatchViaLanguagesButton.Opacity = 1;
                break;
        }
    }

    private void ImageButton_OnClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private async void MainButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainMobilePage)}");
    }

    private async void TVButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainTVPage)}");
    }

    private async void FilmsButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainFilmsPage)}");
    }

    private async void NewsButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainNewAndPopularPage)}");
    }

    private async void MyListButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainMyListPage)}");
    }

    private async void WatchViaLanguagesButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(MainWatchViaLanguagesPage)}");
    }
}