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
using VideoDemos.Views.Mobile.Main;

namespace VideoDemos.Views.Mobile.History;

public partial class HistoryMobilePage : ContentPage
{
    private List<DB_ProggressBanner> _proggressBanners;

    public HistoryMobilePage()
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
            MainContainer.Add(HistoryBannerFactory.CreateMobileBanner(banner));
            _proggressBanners.Add(banner);
        }
        FooterLayout.Add(FooterMobileFactory.CreateFooter(nameof(HistoryMobilePage)));
        NavBarGird.Add(FooterMobileFactory.CreateNavbar());
    }
}