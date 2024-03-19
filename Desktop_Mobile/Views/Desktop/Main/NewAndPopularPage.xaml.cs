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

namespace VideoDemos.Views.Main;

public partial class NewAndPopularPage : ContentPage
{
    private int currentPage = 0;
    private string requstLink = Config.API_LINK+"/titles/popular?";

    public NewAndPopularPage()
    {
        InitializeComponent();
        GenerateBanners();
    }

    private void GenerateBanners()
    {
        foreach (Title banner in JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(requstLink+$"count=20&page={currentPage}")))
        {   
            MainContainer.Add(BannerFactory.CreateBanner(banner));
        }
    }
    private void LoadMoreButton_OnClicked(object sender, EventArgs e)
    {
        currentPage++;
        GenerateBanners();
    }
}