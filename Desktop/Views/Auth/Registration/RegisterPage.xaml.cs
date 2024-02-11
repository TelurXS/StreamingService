﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.StartRegister(LoginEntry.Text, PasswordEntry.Text);
        await Shell.Current.GoToAsync($"/{nameof(RegisterSecondPage)}");
    }

    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");
    }
}