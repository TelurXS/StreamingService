using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class RegisterSecondPage : ContentPage
{
    public RegisterSecondPage()
    {
        InitializeComponent();
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Nickname = NicknameEntry.Text;
        RegisterService.AccountModel.Name = NameEntry.Text;
        RegisterService.AccountModel.Surname = SurnameEntry.Text;
        RegisterService.AccountModel.Birthdate = BirthDateEntry.Date;
        await Shell.Current.GoToAsync($"/{nameof(GenreChoosePage)}");
    }

    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");
    }
}