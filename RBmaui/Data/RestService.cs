using Newtonsoft.Json;
using RBmaui.Models;
using System.Net.Http.Json;

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
                // Testat smerge pe: https://localhost:7083/api/meniuapi
                var list = await _httpClient.GetFromJsonAsync<List<Meniu>>("api/meniuapi");
                return list ?? new List<Meniu>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eroare GetMeniuAsync: {ex.Message}");
                return new List<Meniu>();
            }
        }

        public async Task<string?> PlaseazaComandaAsync(string userEmail, List<CosItem> items)
        {
            var dto = new
            {
                UserEmail = userEmail,
                Items = items.Select(i => new
                {
                    MeniuId = i.Produs.Id,
                    Cantitate = i.Cantitate
                }).ToList()
            };

            var response = await _httpClient.PostAsJsonAsync("api/comenziapi", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return result?["numarComanda"];
        }

        public async Task<List<ComandaAfisare>> GetComenzileMeleAsync(string email)
        {
            var response = await _httpClient.GetStringAsync($"api/comenziapi/{email}");
            var list = JsonConvert.DeserializeObject<List<ComandaAfisare>>(response);
            return list ?? new List<ComandaAfisare>();
        }


    }
}
