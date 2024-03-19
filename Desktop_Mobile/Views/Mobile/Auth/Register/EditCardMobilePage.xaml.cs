using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using VideoDemos.Core.Auth;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class EditCardMobilePage : ContentPage
{
    public EditCardMobilePage()
    {
        InitializeComponent();
        BindingContext = this;

        PlanDetailsGrid.BindingContext = RegisterService.AccountModel.Plan;
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.CardNumber = CardNumberEntry.Text;
        RegisterService.AccountModel.CardDate = ExpDateEntry.Text;
        RegisterService.AccountModel.Cvv = CvvEntry.Text;

        await Shell.Current.GoToAsync($"/{nameof(SuccessfullyEndMobilePage)}");
    }
}