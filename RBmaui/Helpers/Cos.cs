using System.Collections.ObjectModel;
using RBmaui.Models;

namespace RBmaui.Helpers
{
    public class Cos
    {
        public ObservableCollection<CosItem> Items { get; } = new ObservableCollection<CosItem>();

        public void Adauga(Meniu produs)
        {
            var existent = Items.FirstOrDefault(x => x.Produs.Id == produs.Id);
            if (existent != null)
            {
                if (existent.Cantitate < 100)
                    existent.Cantitate++;
            }
            else
            {
                Items.Add(new CosItem { Produs = produs, Cantitate = 1 });
            }
        }

        public void Sterge(CosItem item)
        {
            Items.Remove(item);
        }

        public void Creste(CosItem item)
        {
            if (item.Cantitate < 100) item.Cantitate++;
        }

        public void Scade(CosItem item)
        {
            if (item.Cantitate > 1) item.Cantitate--;
        }

        public decimal Total()
        {
            return Items.Sum(x => x.Subtotal);
        }

        public void Goleste()
        {
            Items.Clear();
        }
    }
}
