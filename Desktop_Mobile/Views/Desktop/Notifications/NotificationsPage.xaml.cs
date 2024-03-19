using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;

namespace VideoDemos.Views.Notifications;

public partial class NotificationsPage : ContentPage
{
    public NotificationsPage()
    {
        InitializeComponent();

        NotificationModel model =
            new NotificationModel("ava_test.png", "Починає стежити за вами", "Підписатись", DateTime.Now);
        model.NotificationHeadline = "Kawazaki";
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
        NotificationContainer.Add(NotificationFactory.CreateNotification(model));
    }

    private async void ChatsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///{nameof(ChatPage)}");
    }
}