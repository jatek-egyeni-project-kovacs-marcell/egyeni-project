using System.Text.Json;
using System.Net.Http.Json; // IMPORTANT
namespace KartyaJatek;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _http = new HttpClient();

    public MainPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        // Set API base URL
        _http.BaseAddress = new Uri("https://localhost:7120/api/Lobby/");
    }

    private async void HostButtonClicked(object sender, EventArgs e)
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "ect.json");

        if (!File.Exists(filePath))
        {
            await DisplayAlert("Error", "ect.json not found", "OK");
            return;
        }

        string json = File.ReadAllText(filePath);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        string id = root.GetProperty("id").GetString();
        string userName = root.GetProperty("userName").GetString();

        // -----------------------------
        // CREATE LOBBY via QUERY STRING
        // -----------------------------
        string url = $"create?lobbyOwner={userName}&playerId={id}";

        HttpResponseMessage res = await _http.PostAsync(url, null);

        if (!res.IsSuccessStatusCode)
        {
            await DisplayAlert($"Error", $"Failed to create lobby, {id} {userName}", "OK");
            return;
        }

        // READ RESULT
        LobbyResponse lobby = await res.Content.ReadFromJsonAsync<LobbyResponse>();

        await DisplayAlert(
            "Lobby Created",
            $"Code: {lobby.Code}\nServerId: {lobby.serverId}",
            "OK"
        );
    }


    // Response model (local class)
    public class LobbyResponse
    {
        public Guid serverId { get; set; }
        public string LobbyOwner { get; set; }
        public string Code { get; set; }
        public List<Guid> PlayerIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool GameStarted { get; set; }
    }

}
