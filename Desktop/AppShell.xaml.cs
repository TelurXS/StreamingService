using VideoDemos.Views;
using VideoDemos.Views.Auth.Registration;
using VideoDemos.Views.Bookmarks;
using VideoDemos.Views.Notifications;
using VideoDemos.Views.Profile.Settings;
using VideoDemos.Views.Profile.Settings.ChangeEmail;
using VideoDemos.Views.Profile.Settings.ChangePassword;
using VideoDemos.Views.Profile.Settings.Payment;

namespace VideoDemos;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ListeningPage), typeof(ListeningPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(VideoPlayerPage), typeof(VideoPlayerPage));

        // REGISTER
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RegisterSecondPage), typeof(RegisterSecondPage));
        Routing.RegisterRoute(nameof(GenreChoosePage), typeof(GenreChoosePage));
        Routing.RegisterRoute(nameof(ChoosePlanPage), typeof(ChoosePlanPage));
        Routing.RegisterRoute(nameof(PayChoosePage), typeof(PayChoosePage));
        Routing.RegisterRoute(nameof(AddCardPage), typeof(AddCardPage));
        Routing.RegisterRoute(nameof(SuccessPage), typeof(SuccessPage));

        // SETTINGS
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
    }
}