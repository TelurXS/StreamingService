using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace Xflick.Views.Desktop.Main;

public partial class SearchPage : ContentPage
{
    private int currentPage = 0;
    private string name;
    private string requstLink = Config.API_LINK + "/titles/by-name?";

    public SearchPage()
    {
        InitializeComponent();
        GenerateBanners();
    }

    private void GenerateBanners()
    {
        List<Title> titles = JsonConvert.DeserializeObject<List<Title>>(
            APIExecutor.ExecuteGet(requstLink +
                                   $"name={NavbarFactory.SearchEntry.Text}&count=20&page={currentPage}"));
        foreach (Title banner in titles)
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