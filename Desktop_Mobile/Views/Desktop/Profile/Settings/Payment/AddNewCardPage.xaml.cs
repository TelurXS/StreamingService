using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace VideoDemos.Views.Profile.Settings.Payment;

public partial class AddNewCardPage : ContentPage
{
    public AddNewCardPage()
    {
        InitializeComponent();
    }

    private async void SaveButtonOnClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }

    private async void CancelButonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }
}