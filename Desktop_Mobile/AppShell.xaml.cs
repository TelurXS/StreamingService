using Microsoft.Maui.Controls;
using VideoDemos.Views;
using VideoDemos.Views.Auth.Registration;
using VideoDemos.Views.Bookmarks;
using VideoDemos.Views.Main;
using VideoDemos.Views.Mobile.Auth;
using VideoDemos.Views.Mobile.Main;
using VideoDemos.Views.Notifications;
using VideoDemos.Views.Profile;
using VideoDemos.Views.Profile.Settings;
using VideoDemos.Views.Profile.Settings.ChangeEmail;
using VideoDemos.Views.Profile.Settings.ChangePassword;
using VideoDemos.Views.Profile.Settings.Payment;
using Xflick.Views.Desktop.Main;
using Xflick.Views.Desktop.Player;

namespace VideoDemos;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        // MAIN PAGES
        Routing.RegisterRoute(nameof(ListeningPage), typeof(ListeningPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));

        //TYPE PAGES
        Routing.RegisterRoute(nameof(FilmsPage), typeof(FilmsPage));
        Routing.RegisterRoute(nameof(SeriesPage), typeof(SeriesPage));
        Routing.RegisterRoute(nameof(NewAndPopularPage), typeof(NewAndPopularPage));
        Routing.RegisterRoute(nameof(WatchViaLanguagesPage), typeof(WatchViaLanguagesPage));


        //VIDEO
        Routing.RegisterRoute(nameof(ConnectedSessionPage), typeof(ConnectedSessionPage));
        Routing.RegisterRoute(nameof(BannerDetailsPage), typeof(BannerDetailsPage));

        // REGISTER
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RegisterSecondPage), typeof(RegisterSecondPage));
        Routing.RegisterRoute(nameof(GenreChoosePage), typeof(GenreChoosePage));
        Routing.RegisterRoute(nameof(ChoosePlanPage), typeof(ChoosePlanPage));
        Routing.RegisterRoute(nameof(PayChoosePage), typeof(PayChoosePage));
        Routing.RegisterRoute(nameof(AddCardPage), typeof(AddCardPage));
        Routing.RegisterRoute(nameof(SuccessPage), typeof(SuccessPage));

        // SETTINGS
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(ProfileEditPage), typeof(ProfileEditPage));
        Routing.RegisterRoute(nameof(AccountSettingsPage), typeof(AccountSettingsPage));
        Routing.RegisterRoute(nameof(ChangeEmailPage), typeof(ChangeEmailPage));
        Routing.RegisterRoute(nameof(ConfirmNewEmailPage), typeof(ConfirmNewEmailPage));
        Routing.RegisterRoute(nameof(ChangePlanPage), typeof(ChangePlanPage));
        Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
        Routing.RegisterRoute(nameof(PaymentControllsPage), typeof(PaymentControllsPage));
        Routing.RegisterRoute(nameof(ReservePaymentMethodPage), typeof(ReservePaymentMethodPage));
        Routing.RegisterRoute(nameof(AddNewCardPage), typeof(AddNewCardPage));
        Routing.RegisterRoute(nameof(PaymentDataPage), typeof(PaymentDataPage));

        Routing.RegisterRoute(nameof(ForgotPasswordPersonConfirm), typeof(ForgotPasswordPersonConfirm));
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));

        // BOOKMARKS
        Routing.RegisterRoute(nameof(BookmarksDetailsPage), typeof(BookmarksDetailsPage));

        // CHATS
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));

        //PROFILE 
        Routing.RegisterRoute(nameof(GenreChangePage), typeof(GenreChangePage));
        Routing.RegisterRoute(nameof(AnotherUserProfilePage), typeof(AnotherUserProfilePage));
    }
}