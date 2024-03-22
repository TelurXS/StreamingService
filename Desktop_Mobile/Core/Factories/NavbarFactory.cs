using System;
using System.Collections.Generic;
using Metflix.Core.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views;
using VideoDemos.Views.Main;
using Xflick.Views.Desktop.Main;

namespace Metflix.Core;

public class NavbarFactory
{
    public static Dictionary<string, string> NavBar = new Dictionary<string, string>()
    {
        { "Головна", "//MainPage" },
        { "Телесеріали", nameof(SeriesPage) },
        { "Фільми", nameof(FilmsPage) },
        { "Новинки й популярне", nameof(NewAndPopularPage) },
        // { "Перегляд за мовами", nameof(WatchViaLanguagesPage) },
    };

    private static Border detailsBorder;
    public static Entry SearchEntry;

    public static Grid CreateNavBar(AuthService authService, bool displayLayout = true)
    {
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);


        Grid navBar = new Grid();
        navBar.ColumnDefinitions = new ColumnDefinitionCollection(
            new[]
            {
                new ColumnDefinition(new GridLength(0.2, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(0.6, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(0.2, GridUnitType.Star)),
            });
        navBar.RowDefinitions = new RowDefinitionCollection(
            new[]
            {
                new RowDefinition(new GridLength(0, GridUnitType.Auto)),
                new RowDefinition(new GridLength(0, GridUnitType.Auto)),
            });

        navBar.HeightRequest = 100;
        Color startColor = Color.FromHex("#080808");
        Color endColor = Colors.Transparent;
        GradientStopCollection gradientStopCollection = new GradientStopCollection();
        gradientStopCollection.Add(new GradientStop(startColor, 0.1f));
        gradientStopCollection.Add(new GradientStop(endColor, 1.0f));
        var gradientBrush = new LinearGradientBrush(
            gradientStopCollection,
            new PointF(0, 0),
            new PointF(0, 1)
        );
        navBar.Background = gradientBrush;

        //logo

        navBar.Children.Add(new Image()
        {
            Source = "logo.png",
            WidthRequest = 202,
            HeightRequest = 34,
            Margin = new Thickness(112, 19, 0, 0),
            HorizontalOptions = LayoutOptions.Start
        });
        if (authService.IsAuthenticated() && displayLayout)
        {
            HorizontalStackLayout layout = new HorizontalStackLayout();
            layout.HorizontalOptions = LayoutOptions.Center;

            //elems
            foreach (var navBarElem in NavBar)
            {
                layout.Children.Add(CreateNavBarLink(navBarElem.Key, navBarElem.Value));
            }

            //search & profile
            HorizontalStackLayout profileSelection = new HorizontalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 20, 0)
            };
            SearchEntry = new Entry()
            {
                Placeholder = "Search...",
                Margin = new Thickness(5, 10),
                WidthRequest = 50
            };
            ImageButton searchButton = new ImageButton()
            {
                Source = "search.png",
                WidthRequest = 26,
                HeightRequest = 26,
                Margin = 20,
            };
            searchButton.Clicked += SearchButtonOnClicked;
            profileSelection.Add(SearchEntry);
            profileSelection.Add(searchButton);

            ImageButton profileDetailsButton = new ImageButton()
            {
                Source = "arrow_down.png",
                WidthRequest = 26,
                HeightRequest = 26,
                Margin = new Thickness(0, 0, 18, 0),
                VerticalOptions = LayoutOptions.Center
            };
            profileDetailsButton.Clicked += ProfileDetailsButtonOnClicked;
            profileSelection.Add(profileDetailsButton);
            profileSelection.Add(new Label()
            {
                Text = profileModel.Name,
                FontSize = 16,
                Margin = new Thickness(0, 0, 25, 0),
                VerticalOptions = LayoutOptions.Center
            });
            profileSelection.Add(new Image()
            {
                Aspect = Aspect.Fill,
                Clip = new RoundRectangleGeometry(new CornerRadius(100), new Rect(0, 0, 70, 70)),
                Source = Config.IMAGE_LINK + profileModel.ProfileImage,
                HeightRequest = 70,
                WidthRequest = 70,
                VerticalOptions = LayoutOptions.Center,
            });
            navBar.Add(layout, 1);
            navBar.Add(profileSelection, 2);
        }

        detailsBorder = new Border()
        {
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 10, 40, 0),
            Background = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Color.FromArgb("#080808"), 0),
                new GradientStop(Colors.Transparent, 1),
            }, new Point(1, 0.5), new Point(0, 0.5)),
            Stroke = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Colors.White, 0),
                new GradientStop(Colors.Transparent, 1),
            }, new Point(1, 1), new Point(0, 0)),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(20, 20, 20, 20)
            },
            Opacity = 0
        };
        VerticalStackLayout verticalStackLayout = new VerticalStackLayout()
        {
        };
        Button account = new Button()
        {
            Text = "Акаунт",
            FontSize = 18,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            BorderWidth = 0,
            Margin = new Thickness(0, 0, 0, 0)
        };
        account.Clicked += async (sender, args) => { await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}"); };
        Button exit = new Button()
        {
            Text = "Вихід",
            FontSize = 18,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            BorderWidth = 0,
            Margin = new Thickness(0, 0, 0, 0)
        };
        exit.Clicked += async (sender, args) => { await Shell.Current.GoToAsync($"//{nameof(AuthPage)}"); };

        verticalStackLayout.Add(account);
        verticalStackLayout.Add(exit);

        detailsBorder.Content = verticalStackLayout;
        navBar.Add(detailsBorder, 2, 1);
        return navBar;
    }

    private static void ProfileDetailsButtonOnClicked(object sender, EventArgs e)
    {
        if (detailsBorder.Opacity == 0)
            detailsBorder.Opacity = 1;
        else
            detailsBorder.Opacity = 0;
    }

    private async static void SearchButtonOnClicked(object sender, EventArgs e)
    {
        if (SearchEntry.Text != "")
        {
            await Shell.Current.GoToAsync($"/{nameof(SearchPage)}");
        }
    }

    private static Button CreateNavBarLink(string key, string value)
    {
        Button button = new Button()
        {
            Text = key,
            TextColor = Colors.White,
            BackgroundColor = Colors.Transparent,
            BorderWidth = 0,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(25, 19, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        };
        button.Clicked += async (sender, args) => { await Shell.Current.GoToAsync($"/{value}", true); };
        return button;
    }
}