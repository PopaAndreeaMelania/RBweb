using RBmaui.Models;

namespace RBmaui.Data
{
    public interface IRestService
    {
        Task<List<Meniu>> GetMeniuAsync();
    }
}
