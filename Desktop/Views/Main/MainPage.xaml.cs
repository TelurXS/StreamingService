using System.Windows.Input;
using Metflix.Core;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views;

public partial class MainPage : ContentPage
{
    public ICommand NavigateCommand { get; private set; }
    private List<Banner> _banners;

    public MainPage()
    {
        InitializeComponent();

        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Popular", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/popular"))));
        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Family", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/by-genre?genre=Family&count=10&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Comedy", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/by-genre?genre=Comedy&count=10&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Action", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/by-genre?genre=Action&count=10&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Fantasy", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/by-genre?genre=Fantasy&count=10&page=0"))));
        MainContentLayout.Children.Add(BannerFactory.CreateBannerCollection("Adventure", JsonConvert.DeserializeObject<List<Title>>(APIExecutor.ExecuteGet(Config.API_LINK+"/titles/by-genre?genre=Adventure&count=10&page=0"))));

        NavigateCommand = new Command<Type>(async (Type pageType) =>
        {
            Page page = (Page)Activator.CreateInstance(pageType);
            await Navigation.PushAsync(page);
        });
        BindingContext = this;

       
        Grid navbar = NavbarFactory.CreateNavBar(new AuthService());
        NavBarGrid.Children.Add(navbar);
    }
}