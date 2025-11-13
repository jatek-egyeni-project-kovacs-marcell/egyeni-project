using Microsoft.Maui.Controls;

namespace KartyaJatek;

public partial class Lobby : ContentPage
{
    public Lobby()
    {
        InitializeComponent();
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        // Create card frame
        var card = new Frame
        {
            CornerRadius = 5,
            HasShadow = true,
            Padding = new Thickness(10),
            Margin = new Thickness(0, 5)
        };

        // Grid for label and button
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        // Label
        var label = new Label
        {
            Text = "Test",
            VerticalOptions = LayoutOptions.Center,
            FontSize = 16
        };
        grid.Add(label, 0, 0);

        // Kick button
        var kickButton = new Button
        {
            Text = "Kick",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 8
        };
        kickButton.Clicked += (s, ev) =>
        {
            PlayerCardsStack.Children.Remove(card);
        };
        grid.Add(kickButton, 1, 0);

        // Add grid to frame
        card.Content = grid;

        // Add card to stack
        PlayerCardsStack.Children.Add(card);
    }
}