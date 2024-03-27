using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Backend;
using Xflick.Core.Models.Notifications;

namespace VideoDemos.Views.Notifications;

public partial class NotificationsPage : ContentPage
{
    public NotificationsPage()
    {
        InitializeComponent();

        List<DB_Notification> notifications = JsonConvert.DeserializeObject<List<DB_Notification>>(APIExecutor.ExecuteGet(Config.API_LINK + "/notifications"));
        foreach (DB_Notification notification in notifications)
        {
            NotificationContainer.Add(NotificationFactory.CreateNotification(notification));
        }

    }
}