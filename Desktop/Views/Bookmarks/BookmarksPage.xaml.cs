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

public partial class BookmarksPage : ContentPage
{
    public BookmarksPage()
    {
        InitializeComponent();
        string result = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
        List<DBBanner> banners = JsonConvert.DeserializeObject<List<DBBanner>>(result);
        foreach (DBBanner banner in banners)
        {
            MainContainer.Add(BookmarksFactory.CreateBanner(banner.Name, Config.IMAGE_LINK + banner.Titles[0].Image.Uri,
                banner.Availability == 1, banner.Id));
        }
    }
}