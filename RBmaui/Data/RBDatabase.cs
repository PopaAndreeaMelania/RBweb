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
    }
}
