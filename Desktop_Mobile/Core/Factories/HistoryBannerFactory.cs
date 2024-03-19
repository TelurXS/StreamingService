using System;
using Metflix.Core.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
// using VideoDemos.Controls;
using VideoDemos.Core.Backend;
using VideoDemos.Views;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;

namespace Metflix.Core;

public class HistoryBannerFactory
{
    private static float bannerWidth = 279;
    private static float bannerHeight = 402;
    private static float mobileBannerWidth = 118;
    private static float mobileBannerHeight = 170;

    public static VerticalStackLayout CreateBanner(DB_ProggressBanner banner)
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();
        mainLayout.WidthRequest = bannerWidth;
        mainLayout.HeightRequest = bannerHeight + 150;
        mainLayout.HorizontalOptions = LayoutOptions.Center;
        mainLayout.VerticalOptions = LayoutOptions.Center;
        mainLayout.Margin = new Thickness(0, 0, 20, 0);
        Grid grid = new Grid();

        grid.WidthRequest = bannerWidth;
        grid.HeightRequest = bannerHeight;
        grid.Margin = 15;

        AcrylicView rating = new AcrylicView()
        {
            WidthRequest = 50,
            HeightRequest = 22,
            CornerRadius = 5,
            EffectStyle = EffectStyle.Dark,
            Margin = new Thickness(20, 20, 0, 0),
            ZIndex = 999,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Padding = 0
        };
        HorizontalStackLayout mark = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        mark.Add(new Label()
        {
            Margin = new Thickness(4, 0, 0, 2),
            Text = banner.Title.AvarageRate.ToString(),
            TextColor = Colors.White,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold
        });
        mark.Add(new Image()
        {
            Source = "star.png",
            WidthRequest = 15,
            HeightRequest = 15,
            Margin = new Thickness(5, 0, 0, 0),
            ZIndex = 999
        });
        rating.Content = mark;

        grid.Add(rating);

        AcrylicView year = new AcrylicView()
        {
            WidthRequest = 43,
            HeightRequest = 22,
            CornerRadius = 5,
            EffectStyle = EffectStyle.Dark,
            Margin = new Thickness(80, 20, 0, 0),
            ZIndex = 999,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
        };
        HorizontalStackLayout year_view = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
        year_view.Add(new Label()
        {
            Text = banner.Title.ReleaseDate.Year.ToString(),
            TextColor = Colors.White,
            Padding = new Thickness(4, 0, 0, 2),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold
        });
        year.Content = year_view;
        grid.Add(year);
        grid.Add(new Image()
        {
            Margin = new Thickness(0, 20, 20, 0),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Source = "info.png",
            WidthRequest = 22,
            HeightRequest = 22,
            ZIndex = 999
        });
        Button eventTrigger = new Button();
        eventTrigger.Background = Brush.Transparent;
        eventTrigger.BorderWidth = 0;
        eventTrigger.Text = banner.Title.Slug;
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;
        Image bannerImage = new Image
        {
            Source = Config.IMAGE_LINK + banner.Title.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, bannerWidth, bannerHeight)),
        };


        grid.Children.Add(bannerImage);

        grid.Children.Add(eventTrigger);


        mainLayout.Children.Add(grid);

        Grid watchProgressGrid = new Grid()
        {
        };
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = bannerWidth,
                HeightRequest = 12,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#303030"),
                CornerRadius = 10
            });

        float bunnerWidth = bannerWidth * banner.Progress;
        if(bunnerWidth < 4f)
        {
            bunnerWidth = 15;
        }
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = bunnerWidth,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#0044E9"),
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.Start
            });
        mainLayout.Children.Add(watchProgressGrid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Title.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }
    public static VerticalStackLayout CreateMobileBanner(DB_ProggressBanner banner)
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();
        mainLayout.WidthRequest = mobileBannerWidth;
        mainLayout.HeightRequest = mobileBannerHeight + 30;
        mainLayout.HorizontalOptions = LayoutOptions.Center;
        mainLayout.VerticalOptions = LayoutOptions.Center;
        mainLayout.Margin = new Thickness(0, 0, 0, 0);
        Grid grid = new Grid();

        grid.WidthRequest = mobileBannerWidth;
        grid.HeightRequest = mobileBannerHeight;
        grid.Margin = 15;
        
        Button eventTrigger = new Button();
        eventTrigger.Background = Brush.Transparent;
        eventTrigger.BorderWidth = 0;
        eventTrigger.Text = banner.Title.Slug;
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;
        Image bannerImage = new Image
        {
            Source = Config.IMAGE_LINK + banner.Title.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = mobileBannerWidth,
            HeightRequest = mobileBannerHeight,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, mobileBannerWidth, mobileBannerHeight)),
        };


        grid.Children.Add(bannerImage);

        grid.Children.Add(eventTrigger);


        mainLayout.Children.Add(grid);

        Grid watchProgressGrid = new Grid()
        {
        };
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = mobileBannerWidth,
                HeightRequest = 12,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#303030"),
                CornerRadius = 10
            });

        float bannerProgress = mobileBannerWidth * banner.Progress;
        if(bannerProgress < 4f)
        {
            bannerProgress = 15;
        }
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = bannerProgress,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#0044E9"),
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.Start
            });
        mainLayout.Children.Add(watchProgressGrid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Title.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = 13,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }


    private static async void WatchClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string videoId = button.Text;
        BannerFactory.NavigatedBanner = videoId;
        await Shell.Current.GoToAsync($"/{nameof(BannerDetailsPage)}");
    }
    private static async void MobileWatchClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string videoId = button.Text;
        BannerFactory.NavigatedBanner = videoId;
        await Shell.Current.GoToAsync($"/{nameof(BannerDetailsPage)}");
    }
    
}