using System.Collections.ObjectModel;
using RBmaui.Models;

namespace RBmaui.ViewModels
{
    public class MeniuViewModel
    {
        public ObservableCollection<Meniu> Produse { get; set; } = new ObservableCollection<Meniu>();

        public async Task IncarcaMeniu()
        {
            Produse.Clear();
            var list = await App.Database.GetMeniuAsync();

            foreach (var p in list)
                Produse.Add(p);
        }
    }
}
