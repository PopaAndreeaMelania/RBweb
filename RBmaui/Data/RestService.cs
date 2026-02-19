using Newtonsoft.Json;
using RBmaui.Models;
using System.Net.Http.Json;
using Microsoft.Maui.Storage;
using System.Net.Http.Headers;

namespace RBmaui.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

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

        private void AddAuthHeader()
        {
            var token = Preferences.Get("auth_token", "");
            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<List<Meniu>> GetMeniuAsync()
        {
            try
            {
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
            try
            {
                AddAuthHeader();

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
                return result != null && result.ContainsKey("numarComanda") ? result["numarComanda"] : null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eroare PlaseazaComandaAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ComandaAfisare>> GetComenzileMeleAsync(string email)
        {
            try
            {
                AddAuthHeader();

                var response = await _httpClient.GetStringAsync($"api/comenziapi/{email}");
                var list = JsonConvert.DeserializeObject<List<ComandaAfisare>>(response);
                return list ?? new List<ComandaAfisare>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eroare GetComenzileMeleAsync: {ex.Message}");
                return new List<ComandaAfisare>();
            }
        }

        public async Task<LoginResponse?> LoginAsync(string email, string password)
        {
            try
            {
                ClearAuthHeader();

                var dto = new { Email = email, Password = password };

                var response = await _httpClient.PostAsJsonAsync("api/authapi/login", dto);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"LOGIN FAIL HTTP {(int)response.StatusCode}: {body}");
                    Preferences.Set("last_login_error", $"HTTP {(int)response.StatusCode}: {body}");
                    return null;
                }

                Preferences.Remove("last_login_error");
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result;
            }
            catch (Exception ex)
            {
                Preferences.Set("last_login_error", ex.Message);
                System.Diagnostics.Debug.WriteLine($"Eroare LoginAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<(bool ok, string msg)> RegisterAsync(string email, string password)
        {
            try
            {
                var dto = new { Email = email, Password = password };

                var response = await _httpClient.PostAsJsonAsync("api/authapi/register", dto);
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return (true, "OK");

                return (false, $"HTTP {(int)response.StatusCode}: {body}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public void ClearAuthHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        public async Task<bool> AdaugaRecenzieAsync(int comandaId, int rating, string comentariu)
        {
            try
            {
                AddAuthHeader();

                var dto = new
                {
                    ComandaID = comandaId,
                    Rating = rating,
                    Comentariu = comentariu
                };

                var response = await _httpClient.PostAsJsonAsync("api/recenziiapi", dto);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Eroare AdaugaRecenzieAsync: {ex.Message}");
                return false;
            }
        }

    }
}
