using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Brush = Microsoft.Maui.Controls.Brush;
using Button = Microsoft.Maui.Controls.Button;
using ColumnDefinition = Microsoft.Maui.Controls.ColumnDefinition;
using ColumnDefinitionCollection = Microsoft.Maui.Controls.ColumnDefinitionCollection;
using GradientStop = Microsoft.Maui.Controls.GradientStop;
using GradientStopCollection = Microsoft.Maui.Controls.GradientStopCollection;
using Grid = Microsoft.Maui.Controls.Grid;
using Image = Microsoft.Maui.Controls.Image;
using LinearGradientBrush = Microsoft.Maui.Controls.LinearGradientBrush;
using RowDefinition = Microsoft.Maui.Controls.RowDefinition;
using RowDefinitionCollection = Microsoft.Maui.Controls.RowDefinitionCollection;

namespace Metflix.Core;

public class BannerDetailsFactory
{
    public static VerticalStackLayout CreateVoz(VozModel voz)
    {
        VerticalStackLayout container = new VerticalStackLayout()
        {
            Margin = new Thickness(5, 0, 0, 0),
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        Button button = new Button()
        {
            Text = voz.Name,
            TextColor = Colors.White,
            BackgroundColor = Colors.Transparent,
            BorderWidth = 0,
            WidthRequest = 214,
            HeightRequest = 40,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 18,
            Margin = 0,
            FontFamily = "Inter"
        };
        button.Clicked += VozChangedButtonOnClicked;
        Rectangle rectangle = new Rectangle()
        {
            WidthRequest = 214,
            HeightRequest = 2,
            Fill = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Color.FromArgb("#000000"), 1),
                new GradientStop(Color.FromArgb("#0044E9"), 0.5f),
                new GradientStop(Color.FromArgb("#0044E9"), 0f),
            }, new Point(0, 0), new Point(1, 1)),
            Margin = 0,
        };
        if (!voz.IsEnabled)
        {
            rectangle.Opacity = 0;
        }

        container.Add(button);
        container.Add(rectangle);
        return container;
    }

    private static void VozChangedButtonOnClicked(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
    }

    public static Grid CreateComment(Comment comment)
    {
        Grid reusltGrid = new Grid()
        {
            Margin = new Thickness(0, 38, 0, 0)
        };
        reusltGrid.ColumnDefinitions = new ColumnDefinitionCollection()
        {
            new ColumnDefinition(95),
            new ColumnDefinition(),
            new ColumnDefinition(),
        };
        reusltGrid.RowDefinitions = new RowDefinitionCollection()
        {
            new RowDefinition(),
            new RowDefinition(),
            new RowDefinition()
        };

        Image avatarImage = new Image
        {
            Source = comment.AvatarUrl,
            Aspect = Aspect.AspectFill,
            WidthRequest = 70,
            HeightRequest = 70,
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 70, 70)),
        };
        reusltGrid.Add(avatarImage, 0, 0);
        reusltGrid.SetRowSpan(avatarImage, 2);

        Label nicknmeLabel = new Label()
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            Text = comment.Nickname
        };
        reusltGrid.Add(nicknmeLabel, 1, 0);

        Label commentTextLabel = new Label()
        {
            FontSize = 20,
            Text = comment.CommentText,
            MaxLines = 5
        };
        reusltGrid.Add(commentTextLabel, 1, 1);
        reusltGrid.SetColumnSpan(commentTextLabel, 2);

        Label dateTextLabel = new Label()
        {
            FontSize = 16,
            TextColor = Color.FromArgb("#838383"),
            Text = comment.CommentDate.ToLongTimeString(),
            HorizontalOptions = LayoutOptions.End
        };
        reusltGrid.Add(dateTextLabel, 2, 0);

        Button answerButton = new Button()
        {
            FontSize = 18,
            TextColor = Color.FromArgb("#b4b4b4"),
            BackgroundColor = Colors.Transparent,
            BorderWidth = 0,
            Text = "Відповісти",
            HorizontalOptions = LayoutOptions.Start
        };
        reusltGrid.Add(answerButton, 1, 2);


        HorizontalStackLayout likeContainer = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        likeContainer.Add(new Label()
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            Text = comment.LikesAmmount.ToString(),
            VerticalOptions = LayoutOptions.Center,
        });
        ImageButton imageButton = new ImageButton()
        {
            Source = "like.png",
            WidthRequest = 30,
            HeightRequest = 30,
            VerticalOptions = LayoutOptions.Center,
        };
        imageButton.Clicked += LikeButtonOnClick;
        likeContainer.Add(imageButton);

        reusltGrid.Add(likeContainer, 2, 2);


        return reusltGrid;
    }

    private static void LikeButtonOnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public static VerticalStackLayout CreateCommentsLayout(List<Comment> comments)
    {
        VerticalStackLayout verticalStackLayout = new VerticalStackLayout();

        foreach (var comment in comments)
        {
            verticalStackLayout.Add(CreateComment(comment));
        }

        return verticalStackLayout;
    }
}