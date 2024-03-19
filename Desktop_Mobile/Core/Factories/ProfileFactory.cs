using System.Collections.Generic;
using Metflix.Core.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using VideoDemos.Core.Backend;

namespace Metflix.Core;

public class ProfileFactory
{
    public static Grid CreateHistoryBanner(DB_ProggressBanner banner)
    {
        Grid mainGrid = new Grid()
        {
            Margin = new Thickness(0, 0, -31, 0),
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
            Source = Config.IMAGE_LINK + banner.Title.Image.Uri,
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

    public static Grid CreateWatchProgress(float precent)
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
                WidthRequest = 3F * precent,
                HeightRequest = 10,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromArgb("#0044E9"),
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.Start
            });
        return watchProgressGrid;
    }

    public static HorizontalStackLayout CreateBannerCollection(List<DB_ProggressBanner> banners)
    {
        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End
        };
        int i = 0;
        foreach (DB_ProggressBanner banner in banners)
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

    public static IView CreateGenre(string genreName)
    {
        Button button = new Button()
        {
            HeightRequest = 29,
            Text = genreName,
            Padding = new Thickness(25,5),
            Margin = new Thickness(0,0,15,0),
            CornerRadius = 20
        };
        return button;
    }
}