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

namespace VideoDemos.Views.Mobile.Bookmarks;

public partial class BookmarksMobilePage : ContentPage
{
    public BookmarksMobilePage()
    {
        InitializeComponent();
        FooterLayout.Add(FooterMobileFactory.CreateFooter(nameof(BookmarksMobilePage)));
        NavBarGird.Add(FooterMobileFactory.CreateNavbar());
        string result = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
        List<DBBanner> banners = JsonConvert.DeserializeObject<List<DBBanner>>(result);
        foreach (DBBanner banner in banners)
        {
            MainContainer.Add(BookmarksFactory.CreateMobileBanner(banner.Name, Config.IMAGE_LINK + banner.Titles[0].Image.Uri,
                banner.Availability == 1, banner.Titles[0].Slug));
        }
    }

}