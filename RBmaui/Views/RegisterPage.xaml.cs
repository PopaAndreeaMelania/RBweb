namespace RBmaui.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim() ?? "";
            var pass = PasswordEntry.Text ?? "";
            var conf = ConfirmEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Eroare", "Completeaza email si parola.", "OK");
                return;
            }

            if (pass.Length < 6)
            {
                await DisplayAlert("Eroare", "Parola trebuie sa aiba minim 6 caractere.", "OK");
                return;
            }

            if (pass != conf)
            {
                await DisplayAlert("Eroare", "Parolele nu coincid.", "OK");
                return;
            }

            var result = await App.Database.RegisterAsync(email, pass);
            if (!result.ok)
            {
                await DisplayAlert("Eroare", $"Register esuat:\n{result.msg}", "OK");
                return;
            }

            await DisplayAlert("Succes", "Cont creat. Te poti loga.", "OK");
            await Navigation.PopAsync();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
