using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class SuccessfullyEndMobilePage : ContentPage
{
    public SuccessfullyEndMobilePage()
    {
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");    }
}