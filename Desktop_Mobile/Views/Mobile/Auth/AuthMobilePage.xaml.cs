using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Views.Mobile.Auth.Register;
using VideoDemos.Views.Mobile.Main;

namespace VideoDemos.Views.Mobile.Auth;

public partial class AuthMobilePage : ContentPage
{
    private readonly AuthService _authService;

    public AuthMobilePage()
    {
        InitializeComponent();
        _authService = new AuthService();
        if (_authService.IsAuthenticated()) _authService.Logout();
    }
    
    
    async void OnSignInClicked(object sender, EventArgs e)
    {
        string result = _authService.Login(LoginEntry.Text, PasswordEntry.Text, RememberMeRadioButton.IsChecked);
        if (result != "" && result.Length < 50)
        {
            ErrorLabel.Text = result;
            return;
        }

        await Shell.Current.GoToAsync($"//{nameof(MainMobilePage)}");
    }

    async void RegiserButton_OnClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(RegisterMobilePage)}");
    }

    private void OnEnterWithGoogleClicked(object sender, EventArgs e)
    {
    }
}