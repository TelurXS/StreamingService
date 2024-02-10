using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoDemos.Core.Auth;

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