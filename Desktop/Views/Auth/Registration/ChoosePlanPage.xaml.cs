using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class ChoosePlanPage : ContentPage
{
    public ChoosePlanPage()
    {
        InitializeComponent();
        PlansContenierLayout.Add(PlanFactory.CreatePlan(new PlanModel(
            "Основний План", "$9.99/місяць",
            "Необмежений доступ до обраного контенту.\nСпільний перегляд в реальному часі з однією особою, використовуючи відео та аудіо дзвінки.\nЕксклюзивні акції та знижки для абонентів.")));
        PlansContenierLayout.Add(PlanFactory.CreatePlan(new PlanModel(
            "Преміум План", "$14.99/місяць",
            "Необмежений доступ до обраного контенту.\nСпільний перегляд в реальному часі з однією особою, використовуючи відео та аудіо дзвінки.\nЕксклюзивні акції та знижки для абонентів.")));
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        RegisterService.AccountModel.Plan = PlanFactory.SelectedPlan;
        await Shell.Current.GoToAsync($"/{nameof(PayChoosePage)}");
    }
}