using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDemos.Views.Profile.Settings.ChangePassword;

public partial class ChangePasswordPage : ContentPage
{
    public ChangePasswordPage()
    {
        InitializeComponent();
    }
    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }

    private async void ForgotPassowrdClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ForgotPasswordPersonConfirm)}");
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }
}