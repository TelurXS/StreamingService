using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class RegisterMobilePage : ContentPage
{
    public RegisterMobilePage()
    {
        InitializeComponent();
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.StartRegister(LoginEntry.Text, PasswordEntry.Text);
        RegisterService.EndProfileRegister();
        
        if (PasswordEntry.Text != PasswordConfirmEntry.Text) RegisterService._errorsList.Add("Passwords not the same");
        if (RegisterService._errorsList.IsNullOrEmpty())
            await Shell.Current.GoToAsync($"/{nameof(RegisterMobileSecondPage)}");
        VerticalStackLayout layout = new VerticalStackLayout();
        foreach (string error in RegisterService._errorsList)
        {
            layout.Add(new Label()
            {
                Text = error,
                FontSize = 18,
                TextColor = Colors.IndianRed
            });
        }
        
        ErrorsContainer.Content = layout;
        if (RegisterService._errorsList.Count == 0)
        {
            await Shell.Current.GoToAsync($"/{nameof(RegisterMobileSecondPage)}");
        }
    }
    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        
        await Shell.Current.GoToAsync($"//{nameof(AuthPage)}");
    }

}