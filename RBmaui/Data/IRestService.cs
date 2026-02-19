using RBmaui.Models;

namespace RBmaui.Data
{
    public interface IRestService
    {
        Task<List<Meniu>> GetMeniuAsync();
        Task<string?> PlaseazaComandaAsync(string userEmail, List<CosItem> items);
        Task<List<ComandaAfisare>> GetComenzileMeleAsync(string email);
        Task<LoginResponse?> LoginAsync(string email, string password);
        Task<(bool ok, string msg)> RegisterAsync(string email, string password);

    }
}
