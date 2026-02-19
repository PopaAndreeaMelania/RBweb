using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RBmaui.Models
{
    public class CosItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _cantitate = 1;
        public int Cantitate
        {
            get => _cantitate;
            set
            {
                if (_cantitate == value) return;
                _cantitate = value;
                OnPropertyChanged();              // Cantitate
                OnPropertyChanged(nameof(Subtotal)); // si Subtotal depinde de cantitate
            }
        }

        public Meniu Produs { get; set; } = new Meniu();

        public decimal Subtotal => Produs.Pret * Cantitate;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
