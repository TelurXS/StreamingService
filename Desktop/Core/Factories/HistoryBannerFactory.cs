using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using VideoDemos.Controls;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;

namespace Metflix.Core;

public class HistoryBannerFactory
{
    private static float bannerWidth = 279;
    private static float bannerHeight = 402;

    public static VerticalStackLayout CreateBanner(Banner banner)
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
            Text = banner.Rating.ToString(),
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
            Text = banner.RealiseYear.ToString(),
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
        eventTrigger.Text = banner.VideoLink;
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;
        Image bannerImage = new Image
        {
            Source = banner.Image,
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
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = bannerWidth / 100 * banner.WatchedPrecent,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#0044E9"),
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.Start
            });
        mainLayout.Children.Add(watchProgressGrid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }

    
    private static async void WatchClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        VideoSource vs = VideoSource.FromUri(button.Text);
        Dictionary<string, object> sendParams = new Dictionary<string, object>();
        await Shell.Current.GoToAsync($"///BannerDetails", true, sendParams);
    }
}