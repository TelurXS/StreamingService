using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;

namespace VideoDemos.Views.Profile.Settings;

public partial class ChangePlanPage : ContentPage
{
    public ChangePlanPage()
    {
        InitializeComponent();
        PlansContenierLayout.Add(PlanFactory.CreatePlan(new PlanModel(
            "Основний План", "$9.99/місяць",
            "Необмежений доступ до обраного контенту.\nСпільний перегляд в реальному часі з однією особою, використовуючи відео та аудіо дзвінки.\nЕксклюзивні акції та знижки для абонентів.")));
        PlansContenierLayout.Add(PlanFactory.CreatePlan(new PlanModel(
            "Преміум План", "$14.99/місяць",
            "Необмежений доступ до обраного контенту.\nСпільний перегляд в реальному часі з однією особою, використовуючи відео та аудіо дзвінки.\nЕксклюзивні акції та знижки для абонентів.")));
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(AccountSettingsPage)}");
    }
}