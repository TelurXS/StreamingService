using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class ChoosePaymentMethodMobilePage : ContentPage
{
    public ChoosePaymentMethodMobilePage()
    {
        InitializeComponent();
    }

    async void PayByCreditCard(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(EditCardMobilePage)}");
    }

    async void PayByApplePay(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Paymentmethod = "applepay";
        await Browser.Default.OpenAsync(new Uri("https://support.apple.com/uk-ua/108398"), BrowserLaunchMode.SystemPreferred);
    }

    async void PayByPayPal(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Paymentmethod = "paypal";
        await Browser.Default.OpenAsync(new Uri("https://www.paypal.com"), BrowserLaunchMode.SystemPreferred);

    }
}