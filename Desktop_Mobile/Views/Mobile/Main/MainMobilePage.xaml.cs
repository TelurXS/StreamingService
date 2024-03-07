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
using VideoDemos.Views.Mobile.Profile;

namespace VideoDemos.Views.Mobile.Main;

public partial class MainMobilePage : ContentPage
{
    public MainMobilePage()
    {
        InitializeComponent();

        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Popular",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/popular?count=6&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Family",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/by-genre?genre=Family&count=6&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Comedy",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/by-genre?genre=Comedy&count=6&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Action",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/by-genre?genre=Comedy&count=6&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Fantasy",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/by-genre?genre=Comedy&count=6&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateMobileBannerCollection("Adventure",
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + "/titles/by-genre?genre=Comedy&count=6&page=0"))));

        FooterLayout.Add(FooterMobileFactory.CreateFooter(nameof(MainMobilePage)));
        NavBarGird.Add(FooterMobileFactory.CreateNavbar());
    }

    private async void ImageButton_OnClicked(object sender, EventArgs e)
    {
            await Shell.Current.GoToAsync($"/{nameof(ProfileMobilePage)}");
    }
}