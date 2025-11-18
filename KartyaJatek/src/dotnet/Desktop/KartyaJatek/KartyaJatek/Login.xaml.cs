using System;
using System.Text.Json;
using System.Net.Http.Json;
using System.IO;
using System.Text;
using KartyaJatek.Models;

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
        Toregbt.IsVisible = false;
        string username = Usernamefield.Text;
        string password = Passwordfield.Text;

        // Basic validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            Loginbt.IsVisible = true;
            Toregbt.IsVisible = true;
            return;
        }

        try
        {
            // GET all players from backend
            string url = "https://localhost:7120/api/players";
            var players = await _httpClient.GetFromJsonAsync<List<Player>>(url);

            if (players == null || players.Count == 0)
            {
                await DisplayAlert("Error", "No players found", "OK");
                Loginbt.IsVisible = true;
                Toregbt.IsVisible = true;
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

                // Safe path for writing app data (cross-platform)
                string filePath = Path.Combine(FileSystem.AppDataDirectory, "ect.json");

                // Create data to write - using the actual Guid Id from your backend model
                var newData = new
                {
                    id = matchedPlayer.Id.ToString(),
                    userName = matchedPlayer.UserName
                };

                try
                {
                    // Serialize and write JSON asynchronously
                    string json = JsonSerializer.Serialize(newData, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(filePath, json);

                    // Read back the file to verify
                    if (File.Exists(filePath))
                    {
                        string readJson = await File.ReadAllTextAsync(filePath);
                        // You can parse it back if needed
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to save data: {ex.Message}", "OK");
                }

                // Navigate to main page
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                Loginbt.IsVisible = true;
                Toregbt.IsVisible = true;
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
        catch (Exception ex)
        {
            Loginbt.IsVisible = true;
            Toregbt.IsVisible = true;
            await DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }
    }

    private async void OnRegClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Register());
    }
}
