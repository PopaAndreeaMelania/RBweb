using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using RBmaui.Models;

namespace RBmaui.ViewModels
{
    public class ComenziViewModel
    {
        public ObservableCollection<ComandaAfisare> Comenzi { get; set; } = new();

        public async Task Incarca()
        {
            Comenzi.Clear();

            var email = Preferences.Get("user_email", "");
            if (string.IsNullOrWhiteSpace(email))
                return;

            var list = await App.Database.GetComenzileMeleAsync(email);

            int nr = 1;
            foreach (var c in list)
            {
                c.NrStrigat = nr++;
                Comenzi.Add(c);
            }
        }
    }
}
