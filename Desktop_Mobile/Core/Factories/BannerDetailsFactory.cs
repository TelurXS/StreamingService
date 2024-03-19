using System;
using System.Collections.Generic;
using Metflix.Core.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using VideoDemos.Core.Backend;
using VideoDemos.Views;
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
    public static string SelectedVoz = "Default";
    private static Rectangle ActiveRec;

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
            Opacity = 0
        };
        if (voz.IsEnabled)
        {
            rectangle.Opacity = 1;
            ActiveRec = rectangle;
        }

        container.Add(button);
        container.Add(rectangle);
        return container;
    }

    private static void VozChangedButtonOnClicked(object sender, EventArgs e)
    {
        Button button = ((Button)(sender));
        VerticalStackLayout par = (VerticalStackLayout)button.RealParent;
        Rectangle rec = (Rectangle)par.Children[1];
        rec.Opacity = 1;
        if (ActiveRec != null && ActiveRec != rec)
        {
            ActiveRec.Opacity = 0;
        }
        ActiveRec = rec;
        SelectedVoz = button.Text;
    }

    public static Grid CreateComment(DB_Comment comment)
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
            Source = Config.IMAGE_LINK + comment.Author.ProfileImage,
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
            Text = comment.Author.Name
        };
        reusltGrid.Add(nicknmeLabel, 1, 0);

        Label commentTextLabel = new Label()
        {
            FontSize = 20,
            Text = comment.Content,
            MaxLines = 5
        };
        reusltGrid.Add(commentTextLabel, 1, 1);
        reusltGrid.SetColumnSpan(commentTextLabel, 2);

        Label dateTextLabel = new Label()
        {
            FontSize = 16,
            TextColor = Color.FromArgb("#838383"),
            Text = comment.ReleaseDate.ToShortDateString(),
            HorizontalOptions = LayoutOptions.End
        };
        reusltGrid.Add(dateTextLabel, 2, 0);

        return reusltGrid;
    }

    public static VerticalStackLayout CreateCommentsLayout(List<DB_Comment> comments)
    {
        VerticalStackLayout verticalStackLayout = new VerticalStackLayout();

        foreach (var comment in comments)
        {
            verticalStackLayout.Add(CreateComment(comment));
        }

        return verticalStackLayout;
    }

    public static Image CreateScreenshot(DB_Image image)
    {
        return new Image()
        {
            Source = Config.IMAGE_LINK + image.Uri,
            WidthRequest = 261,
            HeightRequest = 151,
            Margin = new Thickness(0, 0, 15, 0),
            Clip = new RoundRectangleGeometry(new CornerRadius(20), new Rect(0, 0, 261, 151))
        };
    }
}