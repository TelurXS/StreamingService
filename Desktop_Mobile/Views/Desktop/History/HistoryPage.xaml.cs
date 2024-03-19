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

namespace VideoDemos.Views;

public partial class HistoryPage : ContentPage
{
    private List<DB_ProggressBanner> _proggressBanners;

    public HistoryPage()
    {
        InitializeComponent();

        _proggressBanners = new List<DB_ProggressBanner>();
        foreach (DB_ProggressBanner banner in JsonConvert.DeserializeObject<List<DB_ProggressBanner>>(
                     APIExecutor.ExecuteGet(Config.API_LINK + "/view-records")))
        {
            bool any = false;
            foreach (var x in _proggressBanners)
            {
                if (x.Title.Name == banner.Title.Name)
                {
                    any = true;
                    break;
                }
            }

            if (any) continue;
            
            MainContainer.Add(HistoryBannerFactory.CreateBanner(banner));
            _proggressBanners.Add(banner);
        }
    }
}