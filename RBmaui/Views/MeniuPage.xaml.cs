using RBmaui.Models;
using RBmaui.ViewModels;

namespace RBmaui.Views
{
    public partial class MeniuPage : ContentPage
    {
        private readonly MeniuViewModel _vm = new MeniuViewModel();

        public MeniuPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.IncarcaMeniu();
        }

        private async void VeziCos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CosPage());
        }

        private async void ComenzileMele_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComenziPage());
        }

        private async void AdaugaInCos_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Meniu produs)
            {
                App.Cos.Adauga(produs);
                await DisplayAlert("Cos", $"Ai adaugat {produs.Denumire}", "OK");
            }

        }
        private async void Cont_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

    }
}
