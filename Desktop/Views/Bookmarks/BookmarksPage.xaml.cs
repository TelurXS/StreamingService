using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;

namespace VideoDemos.Views;

public partial class BookmarksPage : ContentPage
{
    public BookmarksPage()
    {
        InitializeComponent();
        MainContainer.Add(BookmarksFactory.CreateBanner("Комедія", "test_bg_1.png", true));
        MainContainer.Add(BookmarksFactory.CreateBanner("Фентезі", "test_bg_2.png", false));
        MainContainer.Add(BookmarksFactory.CreateBanner("Аніме", "test_bg_2.png", false));
    }
}