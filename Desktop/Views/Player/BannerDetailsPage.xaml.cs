using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using VideoDemos.Controls;
using VideoDemos.Core.Auth;
using Xe.AcrylicView;
using Xe.AcrylicView.Controls;


namespace VideoDemos.Views;

[QueryProperty(nameof(FilmsDetails), "FilmsDetails")]
public partial class BannerDetailsPage : ContentPage
{
    public static List<Comment> _comments = new List<Comment>()
    {
        new Comment("Kawazaki", "ava_test.png",
            "Серіал мені дуже сподобався, не дивлячись на те що трилери не мій жанр, а мимо корейських я зовсім проходила, але цей мене вразив. Я б хотіла меньше жорстоких сцен, але розумію що без них меньше сенсу. Є над чим поміркувати і після фільма і під час перегляду, рекомендую!",
            5, DateTime.Now),
        new Comment("Cago", "ava_test.png",
            "Звичайний серіал. Він не шедевр, але і не зовсім вже сміття. Чи вартий він того хайпу, який трапився? Ні. Це явно не \"Гра престолів\" від Південної Кореї. І тим не менше саме для Південної Кореї серіал непоганий. Якщо краще опрацюють сюжетну лінію, головних і другорядних героїв, то у серіалу є шанс стати дуже хорошим у другому сезоні. Це справді диво, що він вистрілив. Особисто для мене 6 з 10.\nІ так, люди тут мають рацію. Тупості в серіалі дійсно багато.",
            5, DateTime.Now),
        new Comment("Krico", "ava_test.png",
            "Серіал просто шикарний, чудовий каст акторів, ігри, стилістика, серіал взагалі не нудний. Порівняно з тим, що зараз за серіали виходять, це як ковток свіжого повітря, щось нове і незвичайне, а не як зазвичай, де більша частина серіалів - це кліше.\nЧекаємо 2 сезон, сподіваюся з'являться нові ігри, інакше буде вже не так цікаво дивитися.",
            5, DateTime.Now),
        new Comment("Estriper", "ava_test.png",
            "Серіал хороший.\nЯ, звісно, запізнився з переглядом (1-го сезону) і тому частину сюжету знав, але все одно було цікаво.\nМені здається, що було б цікавіше, якби переможець не був таким очевидним,\nа то з першої серії зрозуміло хто \"головний герой\".\nПодивимося що буде в другому сезоні ...",
            5, DateTime.Now),
    };

    public static FilmsDetails FilmsDetails = new()
    {
        Actors =
            "Ли Джон-джэ, Пак Хэ-су, О Ён-су, Чон Хо-ён, Хо Сон-тхэ, Анупам Трипати, Ким Джу Рён, Ви Ха-джун, Ю Сон-джу, Ли Ю-мия та інші",
        Mark = 8.5,
        Title = "Гра в кальмара",
        Rateings = "IMDb: 8.0       RottenTomatoes: 95%",
        Type = "Серіал",
        Id = 1,
        Country = "Південна Корея",
        Details =
            "47-річний Сон Гі-Хун, залежний від азартних ігор, живе на доходи своєї хворої матері. Через його пристрасть до азартних ігор і невимушене ставлення до грошей він опинився у фінансовій ямі, і не може отримати опіку над своєю донькою Га-Єн. Коли кредитори погрожують Сону фізичною розправою, якщо чоловік не поверне гроші до кінця місяця, загадковий незнайомець пропонує йому взяти участь у секретній грі на виживання. 456 учасникам доведеться вести боротьбу за солідний грошовий приз, граючи в дитячі ігри...",
        Director = "Хван Дон-хёк",
        Genre = "Трилери, Драми, Бойовики, Детективи",
        Old = "18+ лише для дорослих",
        ReliseDate = "17 вересня 2021 року",
        VideoLink = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
        TrailerLink = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
        SeasonCount = 3,
        EpisodesCount = 12
    };

    public static int CurrentEpisode = 1;

    public static List<VozModel> _voz = new List<VozModel>()
    {
        new VozModel("HDrezka Studio", "HDrezka Studio", true),
        new VozModel("Дубляж", "Дубляж"),
        new VozModel("Оригінал (+субтитри)", "Оригінал (+субтитри)"),
    };

    private static bool _isSeasonChooseing = false;
    private static bool _isEpisodeChooseing = false;

    public BannerDetailsPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        DetailsGrid.BindingContext = FilmsDetails;
        RateingContainer.BindingContext = FilmsDetails;
        NavBarGrid.Children.Add(NavbarFactory.CreateNavBar(new AuthService()));
        DescriptionLabel.Text = $"Про що {FilmsDetails.Type.ToLower()} \"{FilmsDetails.Title}\":";
        FilmsForYouLayout.Children.Add(BannerFactory.CreateBannerCollection("Фільми для вас", new List<Banner>()
        {
            new Banner(0, "big_buck_bunny.png", "Big Buck Bunny",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
                "Big Buck Bunny tells the story of a giant rabbit with a heart bigger than himself. When one sunny day three rodents rudely harass him, something snaps... and the rabbit ain't no bunny anymore!"),
            new Banner(1, "elephant_dream.png", "Elephant Dream",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                "The first Blender Open Movie from 2006"),
            new Banner(2, "for_bigger_blaze.png", "For Bigger Blazes",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4",
                "HBO GO now works with Chromecast -- the easiest way to enjoy online video on your TV. For when you want to settle into your Iron Throne to watch the latest episodes. For $35.\\nLearn how to use Chromecast with HBO GO and more at google.com/chromecast."),
            new Banner(3, "for_big_escape.png", "For Bigger Escape",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
                "ntroducing Chromecast. The easiest way to enjoy online video and music on your TV—for when Batman's escapes aren't quite big enough. For $35. Learn how to use Chromecast with Google Play Movies and more at google.com/chromecast."),
            new Banner(4, "for_bigger_fun.png", "For Bigger Fun",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
                "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
            new Banner(5, "for_bigger_fun.png", "For Bigger Fun",
                "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
                "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
        }));

        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);
        Shell.SetFlyoutBehavior(Application.Current.MainPage, FlyoutBehavior.Flyout);
        TitleNameLabel.Text = FilmsDetails.Title;

        foreach (VozModel vozModel in _voz)
        {
            VozContainerLayout.Add(BannerDetailsFactory.CreateVoz(vozModel));
        }

        CommentsContainer.Content = BannerDetailsFactory.CreateCommentsLayout(_comments);
        
        video.Stop();
        VideoSourceChanged();
    }


#if WINDOWS
    private Microsoft.UI.Windowing.AppWindow GetAppWindow(MauiWinUIWindow window)
    {
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
        return appWindow;
    }
#endif
    private void VideoSourceChanged()
    {
        video.Source = VideoSource.FromUri(FilmsDetails.VideoLink);
        video.AutoPlay = false;
    }

    void OnPlayPauseButtonClicked(object sender, EventArgs args)
    {
        if (video.Status == VideoStatus.Playing)
        {
            video.Pause();
        }
        else if (video.Status == VideoStatus.Paused)
        {
            video.Play();
        }
    }

    void OnContentPageUnloaded(object sender, EventArgs e)
    {
        video.Handler?.DisconnectHandler();
    }

    private void OnMinus10SecondsButtonClicked(object sender, EventArgs e)
    {
        video.Position = video.Position.Add(new TimeSpan(0, 0, 0, -10));
    }

    private void OnPlus10SecondsButtonClicked(object sender, EventArgs e)
    {
        video.Position = video.Position.Add(new TimeSpan(0, 0, 0, 10));
    }

    private void OnNextEpisodeClicked(object sender, EventArgs e)
    {
        video.Stop();
        // video.Source = VideoSource.FromUri(_videoModel.NextEpisode.Url);
        video.Play();
    }

    private void OnEpisodesButtonClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSubtitleButtonClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPlaySpeedButtonClicked(object sender, EventArgs e)
    {
    }

    private void OnFullScreenButtonClicked(object sender, EventArgs e)
    {
#if WINDOWS
        var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;

        var appWindow = GetAppWindow(window);

        switch (appWindow.Presenter)
        {
            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                if (overlappedPresenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
                {
                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                    overlappedPresenter.Restore();
                }
                else
                {
                    overlappedPresenter.SetBorderAndTitleBar(false, false);
                    overlappedPresenter.Maximize();
                }

                break;
        }
#endif
    }


    private void EpisodeButton_OnClicked(object sender, EventArgs e)
    {
        if (FilmsDetails.EpisodesCount > 1)
        {
            if (!_isEpisodeChooseing)
            {
                int opacity = 100;
                for (int i = 0; i < FilmsDetails.EpisodesCount; i++)
                {
                    if (i > 5) opacity -= 10;
                    EpisodesContainer.Children.Add(CreateAcrilicButton("Серiя " + (i + 1), opacity));
                }

                _isEpisodeChooseing = true;
            }
            else
            {
                EpisodesContainer.Children.Clear();
                _isEpisodeChooseing = false;
            }
        }
    }

    private AcrylicView CreateAcrilicButton(string text, int opacity)
    {
        AcrylicView acrylicView = new AcrylicView
        {
            EffectStyle = EffectStyle.ExtraDark,
            WidthRequest = 190,
            HeightRequest = 50,
            Margin = new Thickness(0, 10, 0, 0),
            Opacity = opacity / 100f,
            CornerRadius = 20,
            BorderThickness = 0,
            Padding = 0,
            TintOpacity = 0.4
        };
        HorizontalStackLayout button_view = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Padding = 0
        };
        Button button = new Button
        {
            FontSize = 18,
            CornerRadius = 20,
            WidthRequest = 190,
            HeightRequest = 50,
            Background = new SolidColorBrush(new Color(0, 0, 0, 0.8f)),
            BorderWidth = 0,
            Text = text
        };
        button_view.Add(button);

        acrylicView.Content = button_view;

        return acrylicView;
    }

    private void SeasonButton_OnClicked(object sender, EventArgs e)
    {
        if (FilmsDetails.SeasonCount > 1)
        {
            if (!_isSeasonChooseing)
            {
                int opacity = 100;
                for (int i = 0; i <= FilmsDetails.SeasonCount; i++)
                {
                    if (i > 5) opacity -= 10;
                    SeasonContainer.Children.Add(CreateAcrilicButton("Сезон " + (i + 1), opacity));
                }

                _isSeasonChooseing = true;
            }
            else
            {
                SeasonContainer.Children.Clear();
                _isSeasonChooseing = false;
            }
        }
    }
}