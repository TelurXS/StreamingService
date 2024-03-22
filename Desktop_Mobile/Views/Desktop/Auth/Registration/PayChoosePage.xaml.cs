using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class PayChoosePage : ContentPage
{
    public PayChoosePage()
    {
        InitializeComponent();
    }

    async void PayByCreditCard(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Paymentmethod = "card";
        await Shell.Current.GoToAsync($"/{nameof(AddCardPage)}");
    }

    async void PayByPayPal(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Paymentmethod = "paypal";
        await Browser.Default.OpenAsync(new Uri("https://www.paypal.com"), BrowserLaunchMode.SystemPreferred);

    }


}