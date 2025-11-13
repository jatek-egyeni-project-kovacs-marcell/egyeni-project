
using System.Net.Http.Json;

namespace KartyaJatek;

public partial class Login 
{
    private readonly HttpClient _httpClient;

    public Login()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Loginbt.IsVisible = false;
        toregbt.IsVisible = false;
        string username = Usernamefield.Text;
        string password = Passwordfield.Text;

        // Basic validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        try
        {
            // GET all players from backend
            string url = "https://kartyagame-epa3gmamakcdbzav.westeurope-01.azurewebsites.net/api/players";
            var players = await _httpClient.GetFromJsonAsync<List<Player>>(url);

            if (players == null || players.Count == 0)
            {
                await DisplayAlert("Error", "No players found", "OK");
                return;
            }

            // Match username and password locally
            var matchedPlayer = players.FirstOrDefault(p =>
                p.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && p.Password == password
            );

            if (matchedPlayer != null)
            {
                await DisplayAlert("Success", "Login successful!", "OK");

                // Navigate to main page and reset navigation stack
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                Loginbt.IsVisible = true;
                toregbt.IsVisible = false;
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
        catch (Exception ex)
        {
            Loginbt.IsVisible = true;
            toregbt.IsVisible = false;
            await DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }
    }

    private async void OnRegClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Register());
    }
}