using Metflix.Core.Models;
using VideoDemos.Controls;
using VideoDemos.Views;
using Microsoft.Maui.Controls.Shapes;
using Path = Microsoft.Maui.Controls.Shapes.Path;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;
using VideoDemos.Core.Backend;

namespace Metflix.Core;

public class BannerFactory
{
    public static string NavigatedBanner;
    private static Title _currentTitle;
    private static Banner _currentBanner;
    private static float bannerWidth = 279;
    private static float bannerHeight = 402;
    private static readonly int BANNER_ON_PAGE_AMMOUNT = 6;

    public static VerticalStackLayout CreateBanner(Banner banner, bool isLast = false, int width = 279,
        int height = 402, int fontSize = 22)
    {
        _currentBanner = banner;
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

    public static VerticalStackLayout CreateBanner(Title banner, bool isLast = false, int width = 279,
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
            Text = banner.ReleaseDate.ToString(),
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


    private static void RollNextPageOfBanners(object sender, EventArgs e)
    {
        List<Banner> banners;
        banners = new List<Banner>()
        {
            new Banner(5, "for_bigger_fun.png", "For Bigger Fun",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
                "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
            new Banner(2, "for_bigger_blaze.png", "For Bigger Blazes",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4",
                "HBO GO now works with Chromecast -- the easiest way to enjoy online video on your TV. For when you want to settle into your Iron Throne to watch the latest episodes. For $35.\\nLearn how to use Chromecast with HBO GO and more at google.com/chromecast."),
            new Banner(3, "for_big_escape.png", "For Bigger Escape",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
                "Introducing Chromecast. The easiest way to enjoy online video and music on your TV—for when Batman's escapes aren't quite big enough. For $35. Learn how to use Chromecast with Google Play Movies and more at google.com/chromecast."),

            new Banner(0, "big_buck_bunny.png", "Big Buck Bunny",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
                "Big Buck Bunny tells the story of a giant rabbit with a heart bigger than himself. When one sunny day three rodents rudely harass him, something snaps... and the rabbit ain't no bunny anymore!"),
            new Banner(1, "elephant_dream.png", "Elephant Dream",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                "The first Blender Open Movie from 2006"),
            new Banner(4, "for_bigger_fun.png", "For Bigger Fun",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
                "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
        };

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
                break;
            }
        }

        foreach (Banner banner in banners)
        {
            var viewbanner = CreateBanner(banner, bannerCount == BANNER_ON_PAGE_AMMOUNT - 1);
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

        grid.Add(new Button()
        {
            Text = "<",
            WidthRequest = 0
        }, 0, 0);

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
}