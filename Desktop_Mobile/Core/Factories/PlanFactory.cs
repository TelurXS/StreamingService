using Metflix.Core.Models;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.UI.Xaml.Controls;
using VideoDemos.Views.Auth.Registration;
using Border = Microsoft.Maui.Controls.Border;
using Button = Microsoft.Maui.Controls.Button;
using Grid = Microsoft.Maui.Controls.Grid;
using RowDefinition = Microsoft.Maui.Controls.RowDefinition;
using RowDefinitionCollection = Microsoft.Maui.Controls.RowDefinitionCollection;

namespace Metflix.Core;

public class PlanFactory
{
    private static PlanDBModel _currentPlan;
    public static PlanDBModel SelectedPlan = null;

    public static IView CreatePlan(PlanDBModel plan)
    {
        _currentPlan = plan;

        Grid layout = new Grid
        {
            Padding = 15,
            RowDefinitions = new RowDefinitionCollection(
                new[]
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                })
        };

        Border border = new Border
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 20
            },
            StrokeThickness = 2,
            Padding = new Thickness(16, 8),
            Stroke = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Color.FromArgb("#0042E2"), 0.0f),
                new GradientStop(Color.FromArgb("#0042E2"), 0.3f),
                new GradientStop(Color.FromArgb("#0042E2"), 1.0f),
            }, new Point(0.5, 0.5), new Point(0, 0)),
            Margin = new Thickness(0, 0, 0, 15)
        };

        Label title = new Label
        {
            FontSize = 22,
            TextColor = Colors.White,
            Text = plan.Name
        };
        layout.Add(title, 0, 0);

        Label price = new Label
        {
            FontSize = 18,
            TextColor = Color.FromArgb("#0044E9"),
            Text = plan.Price.ToString()
        };
        layout.Add(price, 0, 1);

        Button eventTrigger = new Button
        {
            BorderWidth = 0,
            BackgroundColor = Colors.Transparent,
            Text = "",
        };
        eventTrigger.Clicked += EventTriggerOnClicked;

        layout.Add(eventTrigger, 0, 0);
        layout.SetRowSpan(eventTrigger, 4);

        border.Content = layout;
        border.BackgroundColor = Colors.Transparent;
        return border;
    }
    public static IView CreateMobilePlan(PlanDBModel plan)
    {
        _currentPlan = plan;

        Grid layout = new Grid
        {
            Padding = 15,
            HeightRequest = 85,
            RowDefinitions = new RowDefinitionCollection(
                new[]
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                })
        };

        Border border = new Border
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 20
            },
            StrokeThickness = 2,
            Padding = new Thickness(16, 8),
            Stroke = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Color.FromArgb("#0042E2"), 0.0f),
                new GradientStop(Color.FromArgb("#0042E2"), 0.3f),
                new GradientStop(Color.FromArgb("#0042E2"), 1.0f),
            }, new Point(1, 1), new Point(0, 0)),
            Margin = new Thickness(0, 0, 0, 15)
        };

        Label title = new Label
        {
            FontSize = 22,
            TextColor = Colors.White,
            Text = plan.Name
        };
        layout.Add(title, 0, 0);

        Label price = new Label
        {
            FontSize = 18,
            TextColor = Color.FromArgb("#0044E9"),
            Text = plan.Price.ToString()
        };
        layout.Add(price, 0, 1);

        Button eventTrigger = new Button
        {
            BorderWidth = 0,
            BackgroundColor = Colors.Transparent,
            Text = "",
        };
        eventTrigger.Clicked += EventTriggerOnClicked;

        layout.Add(eventTrigger, 0, 0);
        layout.SetRowSpan(eventTrigger, 4);

        border.Content = layout;
        border.BackgroundColor = Colors.Transparent;
        return border;
    }

    private static void EventTriggerOnClicked(object sender, EventArgs e)
    {
        Grid parentGrid = (Grid)(((Button)sender).Parent);
        Border parentBorder = (Border)(parentGrid.Parent);
        if (SelectedPlan == null)
        {
            if (parentBorder.BackgroundColor == Colors.Transparent)
            {
                Label textBlock = new Label()
                {
                    Text = "",
                    FontSize = 14,
                };
                parentGrid.Add(textBlock, 0, 2);
                parentBorder.BackgroundColor = Color.FromArgb("#0042E2");
                parentBorder.Stroke = new LinearGradientBrush(
                    new GradientStopCollection()
                    {
                        new GradientStop(Colors.White, 0),
                        new GradientStop(Color.FromArgb("#0042E2"), 0),
                    }, new Point(1, 1), new Point(0, 0));
                SelectedPlan = _currentPlan;
            }
        }
        else if (parentBorder.BackgroundColor != Colors.Transparent)
        {
            parentBorder.Stroke = new LinearGradientBrush(new GradientStopCollection()
            {
                new GradientStop(Color.FromArgb("#0042E2"), 0.0f),
                new GradientStop(Color.FromArgb("#0042E2"), 0.3f),
                new GradientStop(Color.FromArgb("#0042E2"), 1.0f),
            }, new Point(0.5, 0.5), new Point(0, 0));
            parentBorder.BackgroundColor = Colors.Transparent;
            foreach (View child in parentGrid.Children.ToList())
            {
                if (Grid.GetRow(child) == 2)
                {
                    parentGrid.Children.Remove(child);
                }
            }

            SelectedPlan = null;
        }
    }
}