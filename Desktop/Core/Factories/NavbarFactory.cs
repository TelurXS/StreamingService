using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;

namespace Metflix.Core;

public class NavbarFactory
{
    public static Dictionary<string, string> NavBar = new Dictionary<string, string>()
    {
        { "Головна", "MainPage" },
        { "Телесеріали", "MainPage" },
        { "Фільми", "MainPage" },
        { "Новинки й популярне", "MainPage" },
        { "Мій список", "MainPage" },
        { "Перегляд за мовами", "MainPage" },
    };

    public static Grid CreateNavBar(AuthService authService, bool displayLayout = true)
    {
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        string bookmarksJson = APIExecutor.ExecuteGet(Config.API_LINK + "/lists");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);


        Grid navBar = new Grid();
        navBar.ColumnDefinitions = new ColumnDefinitionCollection(
            new[]
            {
                new ColumnDefinition(new GridLength(0.2, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(0.6, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(0.2, GridUnitType.Star)),
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
                Margin = new Thickness(0,0,20,0)
            };
            ImageButton searchButton = new ImageButton()
            {
                Source = "search.png",
                WidthRequest = 26,
                HeightRequest = 26,
                Margin = 20,
            };
            searchButton.Clicked += SearchButtonOnClicked;
            profileSelection.Add(searchButton);
            //

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
        else if (!authService.IsAuthenticated())
        {
            navBar.FlowDirection = FlowDirection.RightToLeft;
            navBar.Children.Add(new Button()
            {
                Text = "Login",
                HeightRequest = 50,
                WidthRequest = 274,
                BorderWidth = 0,
                CornerRadius = 15,
                BackgroundColor = Color.FromArgb("#0044E980"),
            });
            navBar.Children.Add(new Button()
            {
                Text = "Register",
                HeightRequest = 50,
                WidthRequest = 274,
                BorderWidth = 0,
                CornerRadius = 15,
                BackgroundColor = new Color(0, 0, 0, 50),
            });
        }

        return navBar;
    }

    private static void ProfileDetailsButtonOnClicked(object sender, EventArgs e)
    {
    }

    private static void SearchButtonOnClicked(object sender, EventArgs e)
    {
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
        button.Clicked += async (sender, args) => { await Shell.Current.GoToAsync($"///{value}", true); };
        return button;
    }
}