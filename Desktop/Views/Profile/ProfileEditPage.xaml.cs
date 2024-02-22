using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Profile;

public partial class ProfileEditPage : ContentPage
{
    public ProfileEditPage()
    {
        InitializeComponent();
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/profile", JsonConvert.SerializeObject(new UserDetails()
        {
            Name = NicknameEntry.Text,
            FirstName = NameEntry.Text,
            SecondName = SurnameEntry.Text,
            BirthDate = BirthDateEntry.Date
        }));
        await Shell.Current.GoToAsync($"..");
    }
}