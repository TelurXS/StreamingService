using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using VideoDemos.Core.Steaming;
using Xflick.Core.Models.Notifications;
using Xflick.Views.Desktop.Player;

namespace Metflix.Core;

public class NotificationFactory
{
    public static Grid CreateNotification(DB_Notification model)
    {
        Grid result = new Grid()
        {
            Margin = new Thickness(0, 25, 0, 0),
            Padding = 0
        };
        HorizontalStackLayout userDetailsLayout = new HorizontalStackLayout();
        userDetailsLayout.Add(new Image()
        {
            Source = model.RelatedUser.ProfileImage,
            WidthRequest = 70,
            HeightRequest = 70,
            Clip = new RoundRectangleGeometry(new CornerRadius(100), new Rect(0, 0, 70, 70)),
            Margin = new Thickness(0, 0, 15, 0),
            VerticalOptions = LayoutOptions.Start
        });
        VerticalStackLayout userNickLayout = new VerticalStackLayout();
        userNickLayout.Add(new Label()
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(0, 0, 0, 5),
            Text = model.Message,
        });
        HorizontalStackLayout detailsLayout = new HorizontalStackLayout();
        detailsLayout.Add(new Label()
        {
            FontSize = 20,
            Text = model.LocalizabledMessage,
        });
        detailsLayout.Add(new Label()
        {
            Text = model.Date.ToShortTimeString(),
            Margin = new Thickness(20, 0, 0, 0)
        });

        userNickLayout.Add(detailsLayout);
        userDetailsLayout.Add(userNickLayout);
        result.Add(userDetailsLayout);
        Button button = new Button()
        {
            Text = "Ok",
            WidthRequest = 178,
            HeightRequest = 50,
            CornerRadius = 20,
            FontSize = 18,
            BackgroundColor = Color.FromArgb("#0044E9"),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
        };
        button.Clicked += (sender, args) => ConnectedSessionPage.ConnectToSessionString = model.Link;
        result.Add(button);

        return result;
    }
}