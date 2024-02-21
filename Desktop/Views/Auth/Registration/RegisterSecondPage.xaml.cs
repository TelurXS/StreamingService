using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;

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
        
        string jsonData = JsonConvert.SerializeObject(new UserDetails()
        {
            Name = NicknameEntry.Text,
            FirstName = NameEntry.Text,
            SecondName = SurnameEntry.Text,
            BirthDate = BirthDateEntry.Date
        });
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/profile", jsonData);
        await Shell.Current.GoToAsync($"/{nameof(GenreChoosePage)}");
    }

    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");
    }
}

public class UserDetails
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("firstName")] public string FirstName { get; set; }
    [JsonProperty("secondName")] public string SecondName { get; set; }
    [JsonProperty("birthDate")] public DateTime BirthDate { get; set; }
}