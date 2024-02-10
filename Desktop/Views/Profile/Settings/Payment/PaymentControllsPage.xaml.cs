using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;

namespace VideoDemos.Views.Profile.Settings.Payment;

public partial class PaymentControllsPage : ContentPage
{
    public PaymentControllsPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        DataListView.ItemsSource = new List<ViewPaymentModel>()
        {
            new("visa.png", "•••• •••• •••• 4297"),
            new("paypal.png", "•••• •••• •••• 1559"),
            new("applepay.png", "•••• •••• •••• 2478"),
        };
        DataListView.ItemTapped += SelectedPaymentToEdit;
    }

    private void SelectedPaymentToEdit(object sender, ItemTappedEventArgs e)
    {
        
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }
}