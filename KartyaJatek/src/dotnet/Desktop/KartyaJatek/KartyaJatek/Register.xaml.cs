
using System.Net.Http.Json;

namespace KartyaJatek;

public partial class Register
{
    private readonly HttpClient _httpClient;

    public Register()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Get values from Entry fields
        string name = Usernamefield.Text;
        string email = Emailfield.Text;
        string password = Passwordfield.Text;

        // Basic validation
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        // Create object to send
        var player = new Player
        {
            UserName = name,
            Score = 0,
            Email = email,
            Password = password,
        };

        try
        {
            string url = "https://kartyagame-epa3gmamakcdbzav.westeurope-01.azurewebsites.net/api/players";

            // POST the player as JSON
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, player);

            if (response.IsSuccessStatusCode)
            {
                // Navigate to login page
                Application.Current.MainPage = new NavigationPage(new Login());
                await DisplayAlert("Success", "Player added!", "OK");

                
            }
            else
            {
                await DisplayAlert("Error", $"Failed to add player. Status: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }
    }
}

// Make sure this matches the backend model
public class Player
{
    public string UserName { get; set; }
    public int Score { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
