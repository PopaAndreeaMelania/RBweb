using System.Net.Http.Json;
using RBmaui.Models;

namespace RBmaui.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

        // Pt emulator: pun 10.0.2.2 in loc de localhost
        private const string BaseUrlWindows = "https://localhost:7083/";
        // private const string BaseUrlAndroid = "https://10.0.2.2:7083/";

        public RestService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(BaseUrlWindows)
            };
        }

        public async Task<List<Meniu>> GetMeniuAsync()
        {
            try
            {
                // Testat merge pe: https://localhost:7083/api/meniuapi
                var list = await _httpClient.GetFromJsonAsync<List<Meniu>>("api/meniuapi");
                return list ?? new List<Meniu>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eroare GetMeniuAsync: {ex.Message}");
                return new List<Meniu>();
            }
        }
    }
}
