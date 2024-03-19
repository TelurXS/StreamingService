using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class SuccessPage : ContentPage
{
    public SuccessPage()
    {
        InitializeComponent();
        TestLabel.Text = RegisterService.AccountModel.Result;
    }


    private async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");
    }
}