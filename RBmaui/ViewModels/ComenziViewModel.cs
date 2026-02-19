using System.Collections.ObjectModel;
using RBmaui.Models;

namespace RBmaui.ViewModels
{
    public class ComenziViewModel
    {
        public ObservableCollection<ComandaAfisare> Comenzi { get; set; } = new();

        public async Task Incarca(string email)
        {
            Comenzi.Clear();
            var list = await App.Database.GetComenzileMeleAsync(email);
            foreach (var c in list)
                Comenzi.Add(c);
        }
    }
}
