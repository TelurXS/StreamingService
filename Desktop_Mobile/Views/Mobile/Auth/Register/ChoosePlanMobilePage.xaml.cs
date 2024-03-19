using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class ChoosePlanMobilePage : ContentPage
{
    public ChoosePlanMobilePage()
    {
        InitializeComponent();
        List<PlanDBModel> plans = JsonConvert.DeserializeObject<List<PlanDBModel>>(APIExecutor.ExecuteGet(Config.API_LINK+"/subscriptions"));
        foreach (var planDbModel in plans)
        {
            PlansContenierLayout.Add(PlanFactory.CreateMobilePlan(planDbModel));
        }
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Plan = PlanFactory.SelectedPlan;
        await Shell.Current.GoToAsync($"/{nameof(ChoosePaymentMethodMobilePage)}");
    }
}