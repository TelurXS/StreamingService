using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoDemos.Views.Profile.Settings.ChangeEmail;

namespace VideoDemos.Views.Profile.Settings;

public partial class ChangeEmailPage : ContentPage
{
    public ChangeEmailPage()
    {
        InitializeComponent();
    }

    private async void ChangePageButtonOnClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ConfirmNewEmailPage)}");
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }
}