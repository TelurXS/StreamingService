using System;
using System.Collections.Generic;
using Metflix.Core.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;
using VideoDemos.Views;
using VideoDemos.Views.Bookmarks;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;
using Xflick.Views.Mobile.Bookmarks;

namespace Metflix.Core;

public class BookmarksFactory
{
    private static int bannerWidth = 258;
    private static int bannerHeight = 371;   
    
    private static int mobileBannerWidth = 118;
    private static int mobileBannerHeight = 170;
    private static string currentGuid;
    public static event EventHandler clickedEvent;

    public static VerticalStackLayout CreateBanner(string name, string imageSrc, bool isPrivate, string list_id)
    {
        currentGuid = list_id;

        VerticalStackLayout resultConteiner = new VerticalStackLayout();
        Grid content = new Grid();

        string imageSource = "lock.png";
        content.Add(new Image()
        {
            Source = "bookmarks_bg_2.png",
            Margin = new Thickness(39, 39, 0, 0),
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
        });
        content.Add(new Image()
        {
            Source = "bookmarks_bg_1.png",
            Margin = new Thickness(19, 19, 0, 0),
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
        });
        content.Add(new Image()
        {
            Source = imageSrc,
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
        });
        if (!isPrivate) imageSource = "unlock.png";
        content.Add(new Image()
        {
            Source = imageSource,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            WidthRequest = 30,
            HeightRequest = 30,
            Margin = new Thickness(0, 30, 40, 0)
        });
        Button eventTrigger = new Button()
        {
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
            Padding = 0,
            Margin = 0,
            Background = Brush.Transparent,
            BorderWidth = 0,
            Text = currentGuid.ToString(),
            FontSize = 0,
            Opacity = 0,
        };

        content.Add(eventTrigger);
        eventTrigger.Clicked += ButtonClicked;

        resultConteiner.Add(content);
        resultConteiner.Add(new Label()
        {
            Text = name,
            HorizontalOptions = LayoutOptions.Center,
        });
        return resultConteiner;
    }  
    public static VerticalStackLayout CreateMobileBanner(string name, string imageSrc, bool isPrivate, string list_id)
    {
        currentGuid = list_id;

        VerticalStackLayout resultContainer = new VerticalStackLayout();
        Grid content = new Grid();

        string imageSource = "lock.png";
        content.Add(new Image()
        {
            Source = "bookmarks_bg_2.png",
            Margin = new Thickness(39, 39, 0, 0),
            WidthRequest = mobileBannerWidth,
            HeightRequest = mobileBannerHeight,
        });
        content.Add(new Image()
        {
            Source = "bookmarks_bg_1.png",
            Margin = new Thickness(19, 19, 0, 0),
            WidthRequest = mobileBannerWidth,
            HeightRequest = mobileBannerHeight,
        });
        content.Add(new Image()
        {
            Source = imageSrc,
            WidthRequest = mobileBannerWidth,
            HeightRequest = mobileBannerHeight,
        });
        if (!isPrivate) imageSource = "unlock.png";
        content.Add(new Image()
        {
            Source = imageSource,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            WidthRequest = 30,
            HeightRequest = 30,
            Margin = new Thickness(0, 30, 40, 0)
        });
        Button eventTrigger = new Button()
        {
            WidthRequest = mobileBannerWidth,
            HeightRequest = mobileBannerHeight,
            Padding = 0,
            Margin = 0,
            Background = Brush.Transparent,
            BorderWidth = 0,
            Text = currentGuid.ToString(),
            FontSize = 0,
            Opacity = 0,
        };

        content.Add(eventTrigger);
        eventTrigger.Clicked += ButtonMobileClicked;

        resultContainer.Add(content);
        resultContainer.Add(new Label()
        {
            Text = name,
            HorizontalOptions = LayoutOptions.Center,
        });
        return resultContainer;
    }

    public static Grid CreateSaveCollectionButton(Guid id, string name, string imageSrc, bool isPrivate)
    {
        string privacy_text = "Приватна";
        string privacy_image_src = "lock.png";
        if (!isPrivate)
        {
            privacy_image_src = "unlock.png";
            privacy_text = "Загальнодоступна";
        }

        Grid grid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitionCollection()
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
            },
            WidthRequest = 510,
            HeightRequest = 104
        };

        HorizontalStackLayout resultContainer = new HorizontalStackLayout()
        {
        };
        Grid content = new Grid();
        content.Add(new Image()
        {
            Source = "bookmarks_bg_2.png",
            Margin = new Thickness(12, 12, 0, 0),
            WidthRequest = 47,
            HeightRequest = 68,
        });
        content.Add(new Image()
        {
            Source = "bookmarks_bg_1.png",
            Margin = new Thickness(6, 6, 0, 0),
            WidthRequest = 47,
            HeightRequest = 68,
        });
        content.Add(new Image()
        {
            Source = imageSrc,
            WidthRequest = 47,
            HeightRequest = 68,
        });
        resultContainer.Add(content);
        VerticalStackLayout verticalStackLayout = new VerticalStackLayout()
        {
            Margin = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        verticalStackLayout.Add(new Label()
        {
            Text = name,
            FontSize = 18,
            Margin = new Thickness(0, 5, 0, 0)
        });
        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout()
        {
        };
        horizontalStackLayout.Add(new Image()
        {
            Source = privacy_image_src,
            WidthRequest = 12,
            HeightRequest = 12,
            Margin = new Thickness(0, 0, 5, 0)
        });
        horizontalStackLayout.Add(new Label()
        {
            Opacity = 0.5,
            Text = privacy_text,
            FontSize = 16
        });
        verticalStackLayout.Add(horizontalStackLayout);
        resultContainer.Add(verticalStackLayout);
        grid.Add(resultContainer, 0);
        Button button = new Button()
        {
            Margin = new Thickness(0, 0, 15, 0),
            CornerRadius = 20,
            Text = "Зберегти",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        Button eventTrigger = new Button()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
            Padding = 0,
            Margin = 0,
            Background = Brush.Transparent,
            BorderWidth = 0,
            Text = id.ToString(),
            Opacity = 0,
            FontSize = 0,
        };
        eventTrigger.Clicked += clickedEvent;

        grid.Add(button, 1);
        grid.Add(eventTrigger, 0);
        grid.SetRowSpan(eventTrigger, 5);
        grid.SetColumnSpan(eventTrigger, 5);

        return grid;
    }
    
    public static VerticalStackLayout CreateDetailsBanner(Title banner)
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
            Text = banner.AvarageRate.ToString(),
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
            Text = banner.ReleaseDate.ToString(),
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
            Source = "bookmarks.png",
            WidthRequest = 22,
            HeightRequest = 22,
            ZIndex = 999
        });
        Button eventTrigger = new Button();
        eventTrigger.Background = Brush.Transparent;
        eventTrigger.BorderWidth = 0;
        eventTrigger.Text = banner.Slug;
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;
        Image bannerImage = new Image
        {
            Source = banner.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = bannerWidth,
            HeightRequest = bannerHeight,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, bannerWidth, bannerHeight)),
        };


        grid.Children.Add(bannerImage);

        grid.Children.Add(eventTrigger);


        mainLayout.Children.Add(grid);
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

    }

    private async static void ButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)(sender);
        currentGuid = button.Text;
        
        Dictionary<string, object> sendParams = new Dictionary<string, object>();
        sendParams.Add("listId", currentGuid);
        BookmarksDetailsPage.CurrentListId = Guid.Parse(currentGuid);
        await Shell.Current.GoToAsync($"/{nameof(BookmarksDetailsPage)}", sendParams);
    }   
    private async static void ButtonMobileClicked(object sender, EventArgs e)
    {
        Button button = (Button)(sender);
        currentGuid = button.Text;
        
        Dictionary<string, object> sendParams = new Dictionary<string, object>();
        sendParams.Add("listId", currentGuid);
        BannerFactory.NavigatedBanner = currentGuid;
        await Shell.Current.GoToAsync($"/{nameof(BookmarksDetailsMobilePage)}", sendParams);
    }
}
