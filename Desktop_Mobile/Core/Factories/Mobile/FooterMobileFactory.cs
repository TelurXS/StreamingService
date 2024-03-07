using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using VideoDemos.Views.Mobile.Bookmarks;
using VideoDemos.Views.Mobile.History;
using VideoDemos.Views.Mobile.Mail;
using VideoDemos.Views.Mobile.Main;
using VideoDemos.Views.Mobile.Profile;

namespace Metflix.Core.Mobile;

public class FooterMobileFactory
{
    public static string CurrentPage { get; set; } = nameof(MainMobilePage);

    public static Border CreateFooter(string currentViewName)
    {
        Border mainContentBorder = new Border()
        {
            BackgroundColor = Color.FromArgb("#080808"),
            HeightRequest = 98,
            StrokeThickness = 0,
            StrokeShape =  new RoundRectangle()
            {
                CornerRadius = 30
            },
            HorizontalOptions = LayoutOptions.Center,
            ZIndex = 9999,
        };
        HorizontalStackLayout resultContainer = new HorizontalStackLayout();
        resultContainer.Add(CreateButton("manpage_navbar.png", "mainpage_activated.png", nameof(MainMobilePage),
            currentViewName));
        resultContainer.Add(CreateButton("profile_navbar.png", "profile_activated.png", nameof(ProfileMobilePage),
            currentViewName));
        resultContainer.Add(CreateButton("bookmarks_navbar.png", "bookmarks_activated.png", nameof(BookmarksMobilePage),
            currentViewName));
        resultContainer.Add(CreateButton("mail_navbar.png", "mail_activated.png", nameof(MailMobilePage),
            currentViewName));
        resultContainer.Add(CreateButton("history_navbar.png", "history_activated.png", nameof(HistoryMobilePage),
            currentViewName));
        mainContentBorder.Content = resultContainer;
        return mainContentBorder;
    }

    public static VerticalStackLayout CreateButton(string imageIcon, string selectedIcon, string viewName,
        string currentView)
    {
        VerticalStackLayout buttonContainer = new VerticalStackLayout()
        {
            Margin=new Thickness(20,10,20,0),
            VerticalOptions = LayoutOptions.Start,
        };
        bool isSelected = currentView == viewName;
        ImageButton button = new ImageButton()
        {
            ZIndex = 9999,
            WidthRequest = 20,
            HeightRequest = 20,
            Source = imageIcon,
            Margin = 4
        };
        button.Clicked  += async (sender, args) => { await Shell.Current.GoToAsync($"//{viewName}"); };;
        buttonContainer.Add(button);
        if (isSelected)
        {
            button.Source = selectedIcon;
            buttonContainer.Add(new RoundRectangle()
            {
                HeightRequest = 5,
                WidthRequest = 26,
                Fill = Color.FromArgb("#0044E9"),
                CornerRadius = 2
            });
        }

        return buttonContainer;
    }


    public static IView CreateNavbar()
    {
        Grid grid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitionCollection()
            {
                new ColumnDefinition(new GridLength(0.2, GridUnitType.Star)),
                new ColumnDefinition(),
            },
            BackgroundColor = Color.FromArgb("#080808"),
            Margin = 0
        };

        ImageButton button = new ImageButton()
        {
            Source = "navbar.png",
            WidthRequest = 40,
            HeightRequest = 40,
            Margin = 0
        };
        button.Clicked += async (sender, args) => { await Shell.Current.GoToAsync($"//{nameof(MainMobilePage)}"); };
        grid.Add(button, 0, 0);

        HorizontalStackLayout layout = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        HorizontalStackLayout search = new HorizontalStackLayout();
        ImageButton searchButton = new ImageButton()
        {
            WidthRequest = 20,
            HeightRequest = 20,
            Source = "search.png",
            VerticalOptions = LayoutOptions.Center
        };
        Entry searchEntry = new Entry()
        {
            Placeholder = "Search films",
            WidthRequest = 200,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(5, -5)
        };

        search.Add(searchEntry);
        search.Add(searchButton);

        ImageButton collapseButton = new ImageButton()
        {
            WidthRequest = 20,
            HeightRequest = 20,
            Source = "collapse_button.png",
            Margin = new Thickness(30, 0, 10, 0),
            VerticalOptions = LayoutOptions.Center
        };
        collapseButton.Clicked += async (sender, args) =>
        {
            await Shell.Current.GoToAsync($"/{nameof(CollapseMobilePage)}");
        };

        layout.Add(search);
        layout.Add(collapseButton);

        grid.Add(layout, 1, 0);

        return grid;
    }
}