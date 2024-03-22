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

namespace VideoDemos.Views.Auth.Registration;

public partial class ChoosePlanPage : ContentPage
{
    public ChoosePlanPage()
    {
        InitializeComponent();
        List<PlanDBModel> plans = JsonConvert.DeserializeObject<List<PlanDBModel>>(APIExecutor.ExecuteGet(Config.API_LINK+"/subscriptions"));
        foreach (var planDbModel in plans)
        {
            PlansContenierLayout.Add(PlanFactory.CreatePlan(planDbModel));
        }
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Plan = PlanFactory.SelectedPlan;
        RegisterService.EndProfileRegister();
        APIExecutor.ExecutePost(Config.API_LINK + $"/manage/payment/{PlanFactory.SelectedPlan.Name}");
        await Shell.Current.GoToAsync($"/{nameof(PayChoosePage)}");
    }
}

public class PlanDBModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int Level { get; set; }
}