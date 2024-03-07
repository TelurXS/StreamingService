using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views;

public partial class HistoryPage : ContentPage
{
    public HistoryPage()
    {
        InitializeComponent();
        foreach (DB_ProggressBanner banner in JsonConvert.DeserializeObject<List<DB_ProggressBanner>>(APIExecutor.ExecuteGet(Config.API_LINK+"/view-records")))
        {
            // DisplayAlert("Alert", banner.Progress.ToString(), "OK");
            MainContainer.Add(HistoryBannerFactory.CreateBanner(banner));
        }
    }
}