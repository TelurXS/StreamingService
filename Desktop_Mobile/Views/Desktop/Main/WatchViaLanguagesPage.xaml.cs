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

public partial class WatchViaLanguagesPage : ContentPage
{
    private int currentPage = 0;

    private int _sorting = 0;

    private string _language = "Ukrainian";

    private string requstLink = Config.API_LINK + "/titles/by-language?";

    public WatchViaLanguagesPage()
    {
        InitializeComponent();
        GenerateBanners();
    }

    private void GenerateBanners()
    {
        foreach (Title banner in JsonConvert.DeserializeObject<List<Title>>(
                     APIExecutor.ExecuteGet(requstLink +
                                            $"language={_language}&sorting={_sorting}&count=20&page={currentPage}")))
        {
            MainContainer.Add(BannerFactory.CreateBanner(banner));
        }
    }

    private void ReGenerateBanners()
    {
        MainContainer.Clear();
        foreach (Title banner in JsonConvert.DeserializeObject<List<Title>>(
                     APIExecutor.ExecuteGet(requstLink +
                                            $"language={_language}&sorting={_sorting}&count=20&page={currentPage}")))
        {
            MainContainer.Add(BannerFactory.CreateBanner(banner));
        }
    }

    private void LoadMoreButton_OnClicked(object sender, EventArgs e)
    {
        currentPage++;
        GenerateBanners();
    }

    private void LanguagePicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (LanguagePicker.SelectedIndex != -1)
        {
            _language = LanguagePicker.Items[LanguagePicker.SelectedIndex];
        }
    }

    private void SortingPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (SortingPicker.SelectedIndex != -1)
        {
            _sorting = SortingPicker.SelectedIndex;
        }
    }
}