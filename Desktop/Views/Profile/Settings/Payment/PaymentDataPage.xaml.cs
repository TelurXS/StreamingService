using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;

namespace VideoDemos.Views.Profile.Settings.Payment;

public partial class PaymentDataPage : ContentPage
{
    public PaymentDataPage()
    {
        InitializeComponent();
        this.BindingContext = new PaymentDataModel(
            new PlanModel(
                "Преміум План", "$14.99/місяць", ""),
            DateTime.Today,
            new List<PaymentModel>()
            {
                CreateTestModel(),
                CreateTestModel(),
                CreateTestModel(),
                CreateTestModel(),
                CreateTestModel()
            });
    }

    private PaymentModel CreateTestModel()
    {
        return new PaymentModel(DateTime.Today, "Сервіс трансляцій", "23.06.23—22.07.23",
            new ViewPaymentModel("visa.png", "•••• •••• •••• 4297"),
            "13,99 USD (+ПДВ 1,00 USD)",
            "14,99 USD");
    }
}