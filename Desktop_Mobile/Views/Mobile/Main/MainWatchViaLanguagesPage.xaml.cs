using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Mobile;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Mobile.Main;

public partial class MainWatchViaLanguagesPage : ContentPage
{
    private int currentPage = 0;
    private string requstLink = Config.API_LINK+"/titles/by-type?";

    public MainWatchViaLanguagesPage()
    {
        InitializeComponent();
        GenerateBanners();
        ((Layout)FooterLayout).Add(FooterMobileFactory.CreateFooter(nameof(MainMobilePage)));
        ((Layout)NavBarGird).Add(FooterMobileFactory.CreateNavbar());
        
        FooterMobileFactory.CurrentPage = nameof(MainWatchViaLanguagesPage);
    }
    private void GenerateBanners()
    {
        foreach (Title banner in JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(requstLink+$"type=1&count=20&page={currentPage}")))
        {   
            MainContainer.Add(BannerFactory.CreateMobileBanner(banner, false, false, 150, 201, 11, 10));
        }
    }
    private void LoadMoreButton_OnClicked(object sender, EventArgs e)
    {
        currentPage++;
        GenerateBanners();
    }
}