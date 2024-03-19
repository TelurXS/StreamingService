using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using VideoDemos.Core.Auth;
using VideoDemos.ViewModels;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views;

public partial class AuthPage : ContentPage
{
    private readonly AuthService _authService;

    public AuthPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
        if (authService.IsAuthenticated()) _authService.Logout();
    }


    async void OnSignInClicked(object sender, EventArgs e)
    {
        string result = _authService.Login(LoginEntry.Text, PasswordEntry.Text, RememberMeRadioButton.IsChecked);
        if (result != "" && result.Length < 50)
        {
            ErrorLabel.Text = result;
            return;
        }

        await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
    }

    async void RegiserButton_OnClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(RegisterPage)}");
    }
}