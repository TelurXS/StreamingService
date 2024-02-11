using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDemos.Views.Profile.Settings.Payment;

public partial class ReservePaymentMethodPage : ContentPage
{
    public ReservePaymentMethodPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void PayByApplePay(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PayByPayPal(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void PayByCreditCard(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AddNewCardPage)}");
    }
}