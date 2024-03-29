﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Auth.Registration;

public partial class AddCardPage : ContentPage
{
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public AddCardPage()
    {
        InitializeComponent();
        BindingContext = this;

        PlanDetailsGrid.BindingContext = RegisterService.AccountModel.Plan;
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.CardNumber = CardNumberEntry.Text;
        RegisterService.AccountModel.CardDate = ExpDateEntry.Text;
        RegisterService.AccountModel.Cvv = CvvEntry.Text;

        await Shell.Current.GoToAsync($"/{nameof(SuccessPage)}");
    }
    
}