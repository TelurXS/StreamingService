using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDemos.Views.Profile.Settings.ChangePassword;

public partial class ForgotPasswordPersonConfirm : ContentPage
{
    public ForgotPasswordPersonConfirm()
    {
        InitializeComponent();
    }

    private async void ChangePageButtonOnClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ResetPasswordPage)}");
    }
}