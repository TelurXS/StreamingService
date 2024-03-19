using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using VideoDemos.Views.Profile.Settings.ChangePassword;
using VideoDemos.Views.Profile.Settings.Payment;

namespace VideoDemos.Views.Profile.Settings;

public partial class AccountSettingsPage : ContentPage
{
    private SettingsDataModel _model = new SettingsDataModel()
    {
        Card4LastDidgits = "4149",
        Email = "howToEndThatShit@x.flick",
        NextPaymentDate = "29.02.2025",
        PlanName = "Преміум План",
        SubscribeFrom = "01.01.1452"
    };
    
    public AccountSettingsPage()
    {
        InitializeComponent();
        
        this.BindingContext = _model;
        CardLabel.Text += _model.Card4LastDidgits;
        PaymentDateLabel.Text += _model.NextPaymentDate;
        SubribedFromDate.Text += _model.SubscribeFrom;
    }

    private void CancelSubscribeButtonClicked(object sender, EventArgs e)
    {
        
    }

    private async void ChangeEmailButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ChangeEmailPage)}");
    }

    private async void ChangePlanButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ChangePlanPage)}");
    }

    private async void ChangePasswordButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ChangePasswordPage)}");
    }

    private async void ManagePaymentDataClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(PaymentControllsPage)}");
    }

    private async void AddReservePaymentMethod(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(ReservePaymentMethodPage)}");
    }   
    private async void PaymentDataClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(PaymentDataPage)}");
    }
}