using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;

namespace Metflix.Core;

public class ProfileFactory
{
    public static Grid CreateHistoryBanner(Banner banner)
    {
        Grid mainGrid = new Grid()
        {
            Margin = new Thickness(0, 0,-31,0),
            HeightRequest = 107,
            WidthRequest = 74,
        };

        Border border = new Border()
        {
            StrokeShape = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 74, 107)),
            VerticalOptions = LayoutOptions.Center,
            Padding = 0,
            HeightRequest = 107,
            WidthRequest = 74,
            Shadow = new Shadow()
            {
                Brush = Brush.Black,
                Offset = new Point(10, 10),
                Opacity = 0.8f
            }
        };
        Image image = new Image()
        {
            Aspect = Aspect.AspectFill,
            HorizontalOptions = LayoutOptions.Center,
            Source = banner.Image,
            VerticalOptions = LayoutOptions.Center,
            ZIndex = 999
        };
        Button trigger = new Button()
        {
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            FontSize = 0
        };

        border.Content = image;
        mainGrid.Add(border);
        mainGrid.Add(trigger);
        return mainGrid;
    }
    public static Grid CreateWatchProgress(int precent)
    {
        Grid watchProgressGrid = new Grid()
        {
        };
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = 361,
                HeightRequest = 12,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#303030"),
                CornerRadius = 10
            });
        watchProgressGrid.Add(
            new RoundRectangle()
            {
                WidthRequest = 361 / 100 * precent,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#0044E9"),
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.Start
            });
        return watchProgressGrid;
    }
    public static HorizontalStackLayout CreateBannerCollection(List<Banner> banners)
    {
        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End
        };
        int i = 0;
        foreach (Banner banner in banners)
        {
            i++;
            horizontalStackLayout.Add(CreateHistoryBanner(banner));
            if (i == 6)
            {
                return horizontalStackLayout;
            }
        }
        return horizontalStackLayout;
        
    }
}