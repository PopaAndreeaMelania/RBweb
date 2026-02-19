using RBmaui.Models;
using Microsoft.Maui.Storage;

namespace RBmaui.Views
{
    public partial class CosPage : ContentPage
    {
        public CosPage()
        {
            InitializeComponent();
            BindingContext = App.Cos;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ActualizeazaTotal();
        }

        private void ActualizeazaTotal()
        {
            TotalLabel.Text = $"Total: {App.Cos.Total():0.00} lei";
        }

        private void Plus_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (CosItem)btn.BindingContext;

            App.Cos.Creste(item);
            ActualizeazaTotal();
        }

        private void Minus_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (CosItem)btn.BindingContext;

            App.Cos.Scade(item);
            ActualizeazaTotal();
        }

        private void Sterge_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (CosItem)btn.BindingContext;

            App.Cos.Sterge(item);
            ActualizeazaTotal();
        }

        private async void PlaseazaComanda_Clicked(object sender, EventArgs e)
        {
            if (!App.Cos.Items.Any())
            {
                await DisplayAlert("Eroare", "Cosul este gol.", "OK");
                return;
            }

            var token = Preferences.Get("auth_token", "");
            var emailSalvat = Preferences.Get("user_email", "");

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(emailSalvat))
            {
                await DisplayAlert("Autentificare", "Trebuie sa te loghezi ca sa plasezi comanda.", "Logheaza-te");
                await Navigation.PushAsync(new LoginPage());
                return;
            }

            var numar = await App.Database.PlaseazaComandaAsync(emailSalvat, App.Cos.Items.ToList());

            if (numar == null)
            {
                await DisplayAlert("Eroare", "Nu s-a putut trimite comanda.", "OK");
                return;
            }

            App.Cos.Goleste();
            ActualizeazaTotal();

            await DisplayAlert("Succes", $"Comanda plasata! Numar: {numar}", "OK");
            await Navigation.PopAsync();
        }
    }
}
