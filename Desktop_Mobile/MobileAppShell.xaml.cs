using VideoDemos.Views;
using VideoDemos.Views.Mobile.Auth;
using VideoDemos.Views.Mobile.Auth.Register;
using VideoDemos.Views.Mobile.Main;
using VideoDemos.Views.Mobile.Player;
using VideoDemos.Views.Mobile.Profile;


namespace VideoDemos;

public partial class MobileAppShell : Shell
{
    public MobileAppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ListeningPage), typeof(ListeningPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(CollapseMobilePage), typeof(CollapseMobilePage));
        Routing.RegisterRoute(nameof(BannerMobileDetailsPage), typeof(BannerMobileDetailsPage));

        //MAIN PAGES
        Routing.RegisterRoute(nameof(AuthMobilePage), typeof(AuthMobilePage));
        Routing.RegisterRoute(nameof(ProfileMobilePage), typeof(ProfileMobilePage));
        Routing.RegisterRoute(nameof(MainFilmsPage), typeof(MainFilmsPage));
        Routing.RegisterRoute(nameof(MainMyListPage), typeof(MainMyListPage));
        Routing.RegisterRoute(nameof(MainTVPage), typeof(MainTVPage));
        Routing.RegisterRoute(nameof(MainWatchViaLanguagesPage), typeof(MainWatchViaLanguagesPage));

        //REGISTER
        Routing.RegisterRoute(nameof(RegisterMobilePage), typeof(RegisterMobilePage));
        Routing.RegisterRoute(nameof(RegisterMobileSecondPage), typeof(RegisterMobileSecondPage));
        Routing.RegisterRoute(nameof(GenreChooseMobilePage), typeof(GenreChooseMobilePage));
        Routing.RegisterRoute(nameof(ChoosePlanMobilePage), typeof(ChoosePlanMobilePage));
        Routing.RegisterRoute(nameof(ChoosePaymentMethodMobilePage), typeof(ChoosePaymentMethodMobilePage));
        Routing.RegisterRoute(nameof(EditCardMobilePage), typeof(EditCardMobilePage));
        Routing.RegisterRoute(nameof(SuccessfullyEndMobilePage), typeof(SuccessfullyEndMobilePage));
    }
}