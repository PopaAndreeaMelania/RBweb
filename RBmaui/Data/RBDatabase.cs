using RBmaui.Models;

namespace RBmaui.Data
{
    public class RBDatabase
    {
        private readonly IRestService _restService;

        public RBDatabase(IRestService restService)
        {
            _restService = restService;
        }

        public Task<List<Meniu>> GetMeniuAsync()
        {
            return _restService.GetMeniuAsync();
        }

        public Task<string?> PlaseazaComandaAsync(string email, List<CosItem> items)
        {
            return _restService.PlaseazaComandaAsync(email, items);
        }

        public Task<List<ComandaAfisare>> GetComenzileMeleAsync(string email)
        {
            return _restService.GetComenzileMeleAsync(email);
        }

        public Task<LoginResponse?> LoginAsync(string email, string password)
        {
            return _restService.LoginAsync(email, password);
        }
        public Task<(bool ok, string msg)> RegisterAsync(string email, string password)
        {
            return _restService.RegisterAsync(email, password);
        }
        public void ClearAuthHeader()
        {
            _restService.ClearAuthHeader();
        }



    }
}
