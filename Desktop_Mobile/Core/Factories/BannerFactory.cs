using System;
using System.Collections.Generic;
using Metflix.Core.Models;
using Microsoft.Maui;
using VideoDemos.Views;
using Microsoft.Maui.Controls.Shapes;
using Path = Microsoft.Maui.Controls.Shapes.Path;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Newtonsoft.Json;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Mobile.Player;

// using VideoDemos.Views.Mobile.Player;

namespace Metflix.Core;

public class BannerFactory
{
    public static string NavigatedBanner;
    private static Title _currentTitle;
    private static Banner _currentBanner;
    private static float bannerWidth = 279;
    private static float bannerHeight = 402;
    private static readonly int BANNER_ON_PAGE_AMMOUNT = 6;

public static VerticalStackLayout CreateBanner(Title banner, bool isLast = false, bool isFirst = false,
        int width = 279,
        int height = 402, int fontSize = 22)
    {
        _currentTitle = banner;
        banner.Image.Uri = Config.IMAGE_LINK + banner.Image.Uri;
        VerticalStackLayout mainLayout = new VerticalStackLayout();
        mainLayout.WidthRequest = width;
        mainLayout.HeightRequest = height + 150;
        mainLayout.HorizontalOptions = LayoutOptions.Center;
        mainLayout.VerticalOptions = LayoutOptions.Center;
        mainLayout.Margin = new Thickness(0, 0, 20, 0);
        Grid grid = new Grid();

        grid.WidthRequest = width;
        grid.HeightRequest = height;
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
            Text = banner.ReleaseDate.Year.ToString(),
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
        eventTrigger.Text = banner.Slug;
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;
        Image bannerImage = new Image
        {
            Source = banner.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = width,
            HeightRequest = height,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, width, height)),
        };


        grid.Children.Add(bannerImage);

        grid.Children.Add(eventTrigger);
        if (isLast)
        {
            ImageButton nextButton = new ImageButton()
            {
                Source = "next_banners_page_button.png",
                WidthRequest = 100,
                HeightRequest = 100,
                CornerRadius = 50,
                Background = new SolidColorBrush(new Color(0, 0, 0, 90)),
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 112, 0),
                VerticalOptions = LayoutOptions.Center,
                Padding = 25
            };
            nextButton.Clicked += RollNextPageOfBanners;
            grid.Children.Add(nextButton);
        }

        if (isFirst)
        {
            ImageButton prewButton = new ImageButton()
            {
                ZIndex = 999,
                Source = "prew_banners_page_button.png",
                WidthRequest = 100,
                HeightRequest = 100,
                CornerRadius = 50,
                Background = new SolidColorBrush(new Color(0, 0, 0, 90)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                Padding = 25
            };
            prewButton.Clicked += RollPrewPageOfBanners;
            grid.Children.Add(prewButton);
        }

        mainLayout.Children.Add(grid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = fontSize,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }

public static VerticalStackLayout CreateMobileBanner(Title banner, bool isLast = false, bool isFirst = false,
        int width = 279, int height = 402, int fontSize = 22, int margin = 20)
{
    _currentTitle = banner;
    banner.Image.Uri = Config.IMAGE_LINK + banner.Image.Uri;

    VerticalStackLayout mainLayout = CreateMainLayout(width, height, margin);
    Grid grid = CreateMobileBannerGrid(width, height, banner, isLast, isFirst);
    mainLayout.Children.Add(grid);
    mainLayout.Children.Add(CreateBannerLabel(banner, fontSize));

    return mainLayout;
}

private static VerticalStackLayout CreateMainLayout(int width, int height, int margin)
{
    VerticalStackLayout mainLayout = new VerticalStackLayout
    {
        WidthRequest = width,
        HeightRequest = height + 150,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center,
        Margin = new Thickness(0, 0, margin, 0)
    };

    return mainLayout;
}

private static Grid CreateMobileBannerGrid(int width, int height, Title banner, bool isLast, bool isFirst)
{
    Grid grid = new Grid
    {
        WidthRequest = width,
        HeightRequest = height,
        Margin = 15
    };

    CreateInfoButton(grid, banner.Slug, width, height, isLast, isFirst);

    Image bannerImage = CreateBannerImage(banner.Image.Uri, width, height);
    grid.Children.Add(bannerImage);

    return grid;
}


private static void CreateInfoButton(Grid grid, string slug, int width, int height, bool isLast, bool isFirst)
{
    Image infoImage = new Image
    {
        Margin = new Thickness(0, 20, 20, 0),
        HorizontalOptions = LayoutOptions.End,
        VerticalOptions = LayoutOptions.Start,
        Source = "info.png",
        WidthRequest = 22,
        HeightRequest = 22,
        ZIndex = 999
    };

    Button eventTrigger = CreateEventTriggerButton(slug, width, height);
    grid.Children.Add(eventTrigger);
    
    grid.Children.Add(infoImage);

    // if (isLast)
    // {
    //     ImageButton nextButton = CreateNavigationButton("next_banners_page_button.png", 100, 100, isEnd: true);
    //     nextButton.Clicked += RollNextPageOfBanners;
    //     grid.Children.Add(nextButton);
    // }
    //
    // if (isFirst)
    // {
    //     ImageButton prewButton = CreateNavigationButton("prew_banners_page_button.png", 100, 100, isEnd: false);
    //     prewButton.Clicked += RollPrewPageOfBanners;
    //     grid.Children.Add(prewButton);
    // }
}

private static Button CreateEventTriggerButton(string slug, int width, int height)
{
    Button eventTrigger = new Button
    {
        Background = Brush.Transparent,
        BorderWidth = 0,
        Text = slug,
        TextColor = new Color(0, 0, 0, 0),
        WidthRequest = width,
        HeightRequest = height,
    };

    eventTrigger.Clicked += MobileWatchClicked;

    return eventTrigger;
}

private static Image CreateBannerImage(string imageUrl, int width, int height)
{
    return new Image
    {
        Source = imageUrl,
        Aspect = Aspect.AspectFill,
        WidthRequest = width,
        HeightRequest = height,
        Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, width, height)),
    };
}

private static ImageButton CreateNavigationButton(string source, int width, int height, bool isEnd)
{
    ImageButton navigationButton = new ImageButton
    {
        Source = source,
        WidthRequest = width,
        HeightRequest = height,
        CornerRadius = 50,
        Background = new SolidColorBrush(new Color(0, 0, 0, 90)),
        HorizontalOptions = isEnd ? LayoutOptions.End : LayoutOptions.Start,
        Margin = isEnd ? new Thickness(0, 0, 112, 0) : new Thickness(10, 0, 0, 0),
        VerticalOptions = LayoutOptions.Center,
        Padding = 25
    };

    return navigationButton;
}

private static Label CreateBannerLabel(Title banner, int fontSize)
{
    return new Label
    {
        Text = banner.Name,
        FontAttributes = FontAttributes.Bold,
        FontSize = fontSize,
        VerticalOptions = LayoutOptions.End,
        Margin = new Thickness(10),
        HorizontalOptions = LayoutOptions.Center
    };
}

    public static VerticalStackLayout CreateFavBanner(Title banner, bool isLast = false, int width = 160,
        int height = 261, int fontSize = 16)
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();
        mainLayout.WidthRequest = width;
        mainLayout.HeightRequest = height + 31;
        mainLayout.HorizontalOptions = LayoutOptions.Center;
        mainLayout.VerticalOptions = LayoutOptions.Center;
        mainLayout.Margin = new Thickness(0, 0, 20, 0);
        Grid grid = new Grid();

        grid.WidthRequest = width;
        grid.HeightRequest = height;
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
            Text = banner.ReleaseDate.Year.ToString(),
            TextColor = Colors.White,
            Padding = new Thickness(4, 0, 0, 2),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold
        });
        year.Content = year_view;
        grid.Add(year);

        Button eventTrigger = new Button();

        eventTrigger.Background = Brush.Transparent;
        eventTrigger.BorderWidth = 0;
        eventTrigger.Text = banner.AvarageRate.ToString();
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += WatchClicked;

        Image bannerImage = new Image
        {
            Source = Config.IMAGE_LINK + banner.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = width,
            HeightRequest = height,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, width, height)),
        };


        grid.Children.Add(bannerImage);
        grid.Children.Add(eventTrigger);

        mainLayout.Children.Add(grid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = fontSize,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }
    public static VerticalStackLayout CreateFavMobileBanner(Title banner, bool isLast = false, int width = 118,
        int height = 170, int fontSize = 16)
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();
        mainLayout.WidthRequest = width;
        mainLayout.HeightRequest = height + 31;
        mainLayout.HorizontalOptions = LayoutOptions.Center;
        mainLayout.VerticalOptions = LayoutOptions.Center;
        mainLayout.Margin = new Thickness(0, 0, 20, 0);
        Grid grid = new Grid();

        grid.WidthRequest = width;
        grid.HeightRequest = height;
        grid.Margin = 15;

        
        Button eventTrigger = new Button();
        eventTrigger.Background = Brush.Transparent;
        eventTrigger.BorderWidth = 0;
        eventTrigger.Text = banner.AvarageRate.ToString();
        eventTrigger.TextColor = new Color(0, 0, 0, 0);
        eventTrigger.WidthRequest = grid.WidthRequest;
        eventTrigger.HeightRequest = grid.HeightRequest;
        eventTrigger.Clicked += MobileWatchClicked;

        Image bannerImage = new Image
        {
            Source = Config.IMAGE_LINK + banner.Image.Uri,
            Aspect = Aspect.AspectFill,
            WidthRequest = width,
            HeightRequest = height,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, width, height)),
        };


        grid.Children.Add(bannerImage);
        grid.Children.Add(eventTrigger);

        mainLayout.Children.Add(grid);
        mainLayout.Children.Add(new Label
        {
            Text = banner.Name,
            FontAttributes = FontAttributes.Bold,
            FontSize = fontSize,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        });


        return mainLayout;
    }


    private static void RollNextPageOfBanners(object sender, EventArgs e)
    {
        int currentPage = 0;
        ImageButton sendButton = (ImageButton)sender;
        FlexLayout conteiner = (FlexLayout)sendButton.Parent.Parent.Parent;
        VerticalStackLayout currentPageLayout = (VerticalStackLayout)conteiner.Parent.Parent.Parent.Parent;
        conteiner.Children.Clear();
        int bannerCount = 0;
        HorizontalStackLayout horizontalStackLayout = (HorizontalStackLayout)currentPageLayout.Children[0];

        for (int i = 0; i < horizontalStackLayout.Children.Count - 1; i++)
        {
            RoundRectangle childRect = (RoundRectangle)(horizontalStackLayout.Children[i]);
            if (childRect.BackgroundColor.ToInt() != Color.FromArgb("4B4B4B").ToInt())
            {
                childRect.BackgroundColor = Color.FromArgb("4B4B4B");
                RoundRectangle newRect = (RoundRectangle)(horizontalStackLayout.Children[i + 1]);
                newRect.BackgroundColor = Color.FromArgb("0044E9");
                currentPage = i+1;
                break;
            }
        }

        if (currentPage > 5) currentPage = 5;
        List<Title> titles =
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/titles/popular?count=6&page={currentPage}"));


        for (int i = 0; i < titles.Count; i++)
        {
            VerticalStackLayout viewbanner;
            if (currentPage > 0)
            {
                viewbanner = CreateBanner(titles[i], bannerCount == BANNER_ON_PAGE_AMMOUNT - 1, i==0);
            }
            else
            {
                viewbanner = CreateBanner(titles[i], bannerCount == BANNER_ON_PAGE_AMMOUNT - 1);
            }
            conteiner.Children.Add(viewbanner);
            bannerCount++;
        }
    }

    private static async void WatchClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string videoId = button.Text;
        NavigatedBanner = videoId;


        await Shell.Current.GoToAsync($"/{nameof(BannerDetailsPage)}");
    }
    private static async void MobileWatchClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string videoId = button.Text;
        NavigatedBanner = videoId;


        await Shell.Current.GoToAsync($"/{nameof(BannerMobileDetailsPage)}");
    }

    public static VerticalStackLayout CreateBannerCollection(string name, List<Title> banners)
    {
        VerticalStackLayout mainLayout = new VerticalStackLayout();

        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(25, 0)
        };

        for (int i = 0; i < 6; i++)
        {
            horizontalStackLayout.Children.Add(new RoundRectangle()
            {
                WidthRequest = 20,
                HeightRequest = 5,
                Margin = new Thickness(5, 0),
                BackgroundColor = Color.FromArgb("4B4B4B"),
                CornerRadius = 20
            });
        }

        ScrollView view = new ScrollView();
        view.Orientation = ScrollOrientation.Neither;
        view.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;

        StackLayout stackLayout = new StackLayout();
        stackLayout.HorizontalOptions = LayoutOptions.Center;
        stackLayout.Margin = 15;

        stackLayout.Children.Add(new Label()
        {
            Text = name,
            FontSize = 25
        });
        FlexLayout layout = new FlexLayout();
        layout.HorizontalOptions = LayoutOptions.Start;
        layout.JustifyContent = FlexJustify.SpaceAround;

        Grid grid = new Grid();
        grid.HorizontalOptions = LayoutOptions.Start;
        grid.VerticalOptions = LayoutOptions.Center;

        grid.ColumnDefinitions =
            new ColumnDefinitionCollection(new ColumnDefinition(), new ColumnDefinition(),
                new ColumnDefinition());


        int bunnerCount = 0;
        foreach (Title banner in banners)
        {
            var viewbanner = CreateBanner(banner, bunnerCount == BANNER_ON_PAGE_AMMOUNT - 1);

            layout.Children.Add(viewbanner);
            bunnerCount++;
        }

        grid.Add(layout);

        stackLayout.Add(grid);

        view.Content = stackLayout;
        RoundRectangle activePage = (RoundRectangle)(horizontalStackLayout.Children[0]);
        activePage.BackgroundColor = Color.FromArgb("0044E9");

        mainLayout.Add(horizontalStackLayout);
        mainLayout.Add(view);
        return mainLayout;
    }
public static VerticalStackLayout CreateMobileBannerCollection(string name, List<Title> banners)
{
    VerticalStackLayout mainLayout = new VerticalStackLayout();
    
    HorizontalStackLayout horizontalStackLayout = CreateIndicatorStackLayout();

    ScrollView view = CreateBannerMobileScrollView(name, banners);

    RoundRectangle activePage = (RoundRectangle)(horizontalStackLayout.Children[0]);
    activePage.BackgroundColor = Color.FromArgb("0044E9");

    mainLayout.Add(horizontalStackLayout);
    mainLayout.Add(view);

    return mainLayout;
}

private static HorizontalStackLayout CreateIndicatorStackLayout()
{
    HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout
    {
        HorizontalOptions = LayoutOptions.End,
        VerticalOptions = LayoutOptions.Center,
        Margin = new Thickness(0, 0)
    };

    for (int i = 0; i < 6; i++)
    {
        horizontalStackLayout.Children.Add(new RoundRectangle
        {
            WidthRequest = 20,
            HeightRequest = 5,
            Margin = new Thickness(5, 0),
            BackgroundColor = Color.FromArgb("4B4B4B"),
            CornerRadius = 20
        });
    }

    return horizontalStackLayout;
}

private static ScrollView CreateBannerScrollView(string name, List<Title> banners)
{
    ScrollView view = new ScrollView
    {
        Orientation = ScrollOrientation.Horizontal,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Always
    };

    StackLayout stackLayout = new StackLayout
    {
        HorizontalOptions = LayoutOptions.Center
    };

    stackLayout.Children.Add(new Label
    {
        Text = name,
        FontSize = 12
    });

    FlexLayout layout = CreateBannerFlexLayout(banners);

    Grid grid = new Grid
    {
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    grid.ColumnDefinitions = new ColumnDefinitionCollection(
        new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition());

    grid.Add(layout);

    stackLayout.Add(grid);

    view.Content = stackLayout;

    return view;
}
private static ScrollView CreateBannerMobileScrollView(string name, List<Title> banners)
{
    ScrollView view = new ScrollView
    {
        Orientation = ScrollOrientation.Horizontal,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Always
    };

    StackLayout stackLayout = new StackLayout
    {
        HorizontalOptions = LayoutOptions.Center
    };

    stackLayout.Children.Add(new Label
    {
        Text = name,
        FontSize = 12,
        Margin = new Thickness(12,0,0,0)
    });

    FlexLayout layout = CreateBannerMobileFlexLayout(banners);

    Grid grid = new Grid
    {
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    grid.ColumnDefinitions = new ColumnDefinitionCollection(
        new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition());

    grid.Add(layout);

    stackLayout.Add(grid);

    view.Content = stackLayout;

    return view;
}

private static FlexLayout CreateBannerFlexLayout(List<Title> banners)
{
    FlexLayout layout = new FlexLayout
    {
        HorizontalOptions = LayoutOptions.Start,
        JustifyContent = FlexJustify.SpaceAround
    };

    int bannerCount = 0;
    foreach (Title banner in banners)
    {
        var viewBanner = CreateBanner(banner, bannerCount == BannerOnPageAmount - 1, false, 231, 333, 12);
        layout.Children.Add(viewBanner);
        bannerCount++;
    }

    return layout;
}

private static FlexLayout CreateBannerMobileFlexLayout(List<Title> banners)
{
    FlexLayout layout = new FlexLayout
    {
        HorizontalOptions = LayoutOptions.Start,
        JustifyContent = FlexJustify.SpaceAround
    };

    int bannerCount = 0;
    foreach (Title banner in banners)
    {
        var viewBanner = CreateMobileBanner(banner, bannerCount == BannerOnPageAmount - 1, false, 231, 333, 12);
        layout.Children.Add(viewBanner);
        bannerCount++;
    }

    return layout;
}

public static int BannerOnPageAmount { get; } = 6;

private static void RollPrewPageOfBanners(object sender, EventArgs e)
    {
        int currentPage = 0;
        ImageButton sendButton = (ImageButton)sender;
        FlexLayout conteiner = (FlexLayout)sendButton.Parent.Parent.Parent;
        VerticalStackLayout currentPageLayout = (VerticalStackLayout)conteiner.Parent.Parent.Parent.Parent;
        conteiner.Children.Clear();
        int bannerCount = 0;
        HorizontalStackLayout horizontalStackLayout = (HorizontalStackLayout)currentPageLayout.Children[0];

        for (int i = 0; i < horizontalStackLayout.Children.Count - 1; i++)
        {
            if (i != 0)
            {
                RoundRectangle childRect = (RoundRectangle)(horizontalStackLayout.Children[i]);
                if (childRect.BackgroundColor.ToInt() != Color.FromArgb("4B4B4B").ToInt())
                {
                    childRect.BackgroundColor = Color.FromArgb("4B4B4B");
                    RoundRectangle newRect = (RoundRectangle)(horizontalStackLayout.Children[i - 1]);
                    newRect.BackgroundColor = Color.FromArgb("0044E9");
                    currentPage = i-1;
                    break;
                }
            }
        }
        if(currentPage < 0) currentPage = 0;

        List<Title> titles =
            JsonConvert.DeserializeObject<List<Title>>(
                APIExecutor.ExecuteGet(Config.API_LINK + $"/titles/popular?count=6&page={currentPage}"));

        for (int i = 0; i < titles.Count; i++)
        {
            VerticalStackLayout viewbanner;
            if (currentPage > 0)
            {
                viewbanner = CreateBanner(titles[i], bannerCount == BANNER_ON_PAGE_AMMOUNT - 1, i==0);
            }
            else
            {
                viewbanner = CreateBanner(titles[i], bannerCount == BANNER_ON_PAGE_AMMOUNT - 1);
            }
            conteiner.Children.Add(viewbanner);
            bannerCount++;
        }

    }

    public static VerticalStackLayout CreateFavBannerCollection(string collectionName, List<Title> banners)
    {
        VerticalStackLayout rootLayout = new VerticalStackLayout()
        {
            Margin = 20,
            WidthRequest = 772,
            Spacing = 0
        };

        Border mainContentBorder = new Border()
        {
            WidthRequest = 772,
            HeightRequest = 512,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 20
            },
            StrokeThickness = 1,
            Stroke = new SolidColorBrush(new Color(255, 255, 255, 0.3f))
        };

        FlexLayout flexLayout = new FlexLayout()
        {
            AlignItems = FlexAlignItems.Start,
            JustifyContent = FlexJustify.Start,
            AlignContent = FlexAlignContent.Start,
            Wrap = FlexWrap.Wrap,
            WidthRequest = 772,
        };

        flexLayout.Background = new LinearGradientBrush(new GradientStopCollection()
        {
            new GradientStop(Color.FromArgb("032F99"), -0.5f),
            new GradientStop(Colors.Black, 0.5f),
            new GradientStop(Color.FromArgb("032F99"), 1.5f),
        }, new Point(1, 1), new Point(0, 0));

        foreach (Title banner in banners)
        {
            var viewbanner = CreateFavBanner(banner, false, 160, 230);
            viewbanner.Margin = new Thickness(10, 0, 0, 20);
            flexLayout.Add(viewbanner);
        }

        mainContentBorder.Content = flexLayout;

        ScrollView scrollView = new ScrollView()
        {
            WidthRequest = 772,
            HeightRequest = 512,
            VerticalScrollBarVisibility = ScrollBarVisibility.Always,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 772, 512)),
        };
        scrollView.Content = flexLayout;

        rootLayout.Add(new Label()
        {
            Text = collectionName,
            FontSize = 22,
            Margin = 10,
        });
        rootLayout.Add(scrollView);

        return rootLayout;
    }    
    public static VerticalStackLayout CreateFavMobileBannerCollection(string collectionName, List<Title> banners)
    {
        VerticalStackLayout rootLayout = new VerticalStackLayout()
        {
            Margin = 20,
            Spacing = 0
        };

        Border mainContentBorder = new Border()
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 20
            },
            StrokeThickness = 1,
            Margin = new Thickness(40,0,0,0),
            Stroke = new SolidColorBrush(new Color(255, 255, 255, 0.3f))
        };

        FlexLayout flexLayout = new FlexLayout()
        {
            AlignItems = FlexAlignItems.Start,
            JustifyContent = FlexJustify.Start,
            AlignContent = FlexAlignContent.Start,
            Wrap = FlexWrap.Wrap,
            WidthRequest = 355,
            HeightRequest = 232,
            Margin = new Thickness(0,0,0,0),
        };

        flexLayout.Background = new LinearGradientBrush(new GradientStopCollection()
        {
            new GradientStop(Color.FromArgb("032F99"), -0.5f),
            new GradientStop(Colors.Black, 0.5f),
            new GradientStop(Color.FromArgb("032F99"), 1.5f),
        }, new Point(1, 1), new Point(0, 0));

        foreach (Title banner in banners)
        {
            var viewbanner = CreateFavMobileBanner(banner, false, 118, 170);
            viewbanner.Margin = new Thickness(10, 0, 0, 20);
            flexLayout.Add(viewbanner);
        }

        mainContentBorder.Content = flexLayout;

        ScrollView scrollView = new ScrollView()
        {
            WidthRequest = 772,
            HeightRequest = 232,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Always,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 772, 232)),
        };
        scrollView.Content = flexLayout;

        rootLayout.Add(new Label()
        {
            Text = collectionName,
            FontSize = 18,
        });
        rootLayout.Add(scrollView);

        return rootLayout;
    }
}