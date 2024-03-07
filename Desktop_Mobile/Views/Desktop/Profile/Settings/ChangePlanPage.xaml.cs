using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Profile.Settings;

public partial class ChangePlanPage : ContentPage
{
    public ChangePlanPage()
    {
        InitializeComponent();
        List<PlanDBModel> plans = JsonConvert.DeserializeObject<List<PlanDBModel>>(APIExecutor.ExecuteGet(Config.API_LINK+"/subscriptions"));
        foreach (var planDbModel in plans)
        {
            PlansContenierLayout.Add(PlanFactory.CreatePlan(planDbModel));
        }
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}