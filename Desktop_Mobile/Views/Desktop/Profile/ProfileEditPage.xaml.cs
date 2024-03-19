using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
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
        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/profile");
        DBProfileModel profileModel = JsonConvert.DeserializeObject<DBProfileModel>(pJson);
        NicknameEntry.Text = profileModel.Name;
        NameEntry.Text = profileModel.FirstName;
        SurnameEntry.Text = profileModel.SecondName;
        BirthDateEntry.Date = profileModel.BirthDate;
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
        await Shell.Current.GoToAsync("..");
    }
}