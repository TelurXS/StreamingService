using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class RegisterMobileSecondPage : ContentPage
{
    public RegisterMobileSecondPage()
    {
        InitializeComponent();
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Nickname = NicknameEntry.Text;
        RegisterService.AccountModel.Name = NameEntry.Text;
        RegisterService.AccountModel.Surname = SurnameEntry.Text;
        RegisterService.AccountModel.Birthdate = BirthDateEntry.Date;

        string jsonData = JsonConvert.SerializeObject(new UserDetails()
        {
            Name = NicknameEntry.Text,
            FirstName = NameEntry.Text,
            SecondName = SurnameEntry.Text,
            BirthDate = BirthDateEntry.Date
        });
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/profile", jsonData);
        await Shell.Current.GoToAsync($"/{nameof(GenreChooseMobilePage)}");
    }

    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;

        await Shell.Current.GoToAsync($"//{nameof(AuthPage)}");
    }
}