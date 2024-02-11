using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using Xe.AcrylicView;

namespace Metflix.Core;

public class ChatFactory
{
    public static EventHandler ChatSelectedEvent;

    public static Grid CreateChat(ChatUserModel chatUserModel)
    {
        Grid resultGrid = new Grid()
        {
            Margin = new Thickness(0,0,0,0)
        };
        HorizontalStackLayout mainLayout = new HorizontalStackLayout();
        float fontOpacity = 0.5f;
        resultGrid.Padding = new Thickness(20, 10);
        mainLayout.Add(new Image()
        {
            Source = chatUserModel.AvatarUrl,
            WidthRequest = 70,
            HeightRequest = 70,
            Clip = new RoundRectangleGeometry(new CornerRadius(100), new Rect(0, 0, 70, 70)),
            Margin = new Thickness(0, 0, 15, 0),
            VerticalOptions = LayoutOptions.Start
        });

        VerticalStackLayout dataLayout = new VerticalStackLayout()
        {
            VerticalOptions = LayoutOptions.Start
        };

        HorizontalStackLayout nicknameLayout = new HorizontalStackLayout()
        {
            Margin = new Thickness(0, 5, 0, 0),
            VerticalOptions = LayoutOptions.Start
        };
        nicknameLayout.Add(new Label()
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(10, 0, 0, 0),
            Text = chatUserModel.Nickname,
            
        });
        if (!chatUserModel.IsReaden)
        {
            nicknameLayout.Add(new Ellipse()
            {
                Fill = new SolidColorBrush(Color.FromArgb("#0044E9")),
                WidthRequest = 16,
                HeightRequest = 16,
                Margin = new Thickness(10, 0), 
                VerticalOptions = LayoutOptions.Start
            });
            fontOpacity = 1;
        }

        resultGrid.Add(new Label()
        {
            FontSize = 16,
            Text = chatUserModel.LastMessageTime,
            Opacity = 0.5,
            WidthRequest = 90,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0,4)

        });
        dataLayout.Add(nicknameLayout);
        dataLayout.Add(new Label()
        {
            FontSize = 20,
            Opacity = fontOpacity,
            MaxLines = 1,
            Text = chatUserModel.LastMessage,
            VerticalOptions = LayoutOptions.Center
        });

        mainLayout.Add(dataLayout);

        resultGrid.Add(mainLayout);
        Button triggerButton = new Button()
        {
            WidthRequest = 455,
            HeightRequest = 70,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            TextColor = Colors.Transparent,
            BorderWidth = 0
        };
        triggerButton.Clicked += ChatSelectedEvent;
        resultGrid.Add(triggerButton);
        return resultGrid;
    }

    public static VerticalStackLayout CreateMessage(ChatMessageModel model)
    {
        if (model.SendByMe)
            return CreateMyComment(model);
        return CreatePartnerComment(model);
    }

    private static VerticalStackLayout CreateMyComment(ChatMessageModel model)
    {
        VerticalStackLayout result = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            WidthRequest = 594,
            Margin = 20,
            Padding = new Thickness(20, 10)
        };
        HorizontalStackLayout mainTextContaier = new HorizontalStackLayout()
        {
            BackgroundColor = Color.FromArgb("#080808")
        };
        mainTextContaier.Add(new Label()
        {
            MaximumWidthRequest = 554,
            FontSize = 20,
            Text = model.Message,
            FontAttributes = FontAttributes.Bold
        });
        result.Add(mainTextContaier);
        result.Add(new Label()
        {
            Opacity = 0.5,
            FontSize = 16,
            Text = model.SendTime.ToShortTimeString(),
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(20, 5)
        });
        return result;
    }

    private static VerticalStackLayout CreatePartnerComment(ChatMessageModel model)
    {
        VerticalStackLayout result = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Start,
            WidthRequest = 594,
            Margin = 20
        };
        AcrylicView acrylicView = new AcrylicView()
        {
            TintOpacity = 0.1f,
            CornerRadius = 20,
            MaximumWidthRequest = 594,
            Padding = new Thickness(20, 10, 20, 10),
        };
        acrylicView.BorderColor = Color.FromRgba(255, 255, 255, 0.1);
        HorizontalStackLayout mainTextContaier = new HorizontalStackLayout();
        mainTextContaier.Add(new Label()
        {
            MaximumWidthRequest = 554,
            FontSize = 16,
            Text = model.Message
        });
        acrylicView.BorderThickness = 3;

        acrylicView.Content = mainTextContaier;
        result.Add(acrylicView);
        result.Add(new Label()
        {
            Opacity = 0.5,
            FontSize = 16,
            Text = model.SendTime.ToShortTimeString(),
            Margin = new Thickness(20, 5)
        });
        return result;
    }
}