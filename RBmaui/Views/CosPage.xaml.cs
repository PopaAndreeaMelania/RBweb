using RBmaui.Models;

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

            string email = "test@client.com"; // temporar, pana facem login

            var numar = await App.Database.PlaseazaComandaAsync(email, App.Cos.Items.ToList());

            if (numar == null)
            {
                await DisplayAlert("Eroare", "Nu s-a putut trimite comanda.", "OK");
                return;
            }

            App.Cos.Goleste();
            await DisplayAlert("Succes", $"Comanda plasata! Numar: {numar}", "OK");
            await Navigation.PopAsync();
        }

    }
}
