using Microsoft.Maui.Storage;

namespace RBmaui.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim() ?? "";
            var pass = PasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Eroare", "Completeaza email si parola.", "OK");
                return;
            }

            var result = await App.Database.LoginAsync(email, pass);

            if (result == null || string.IsNullOrWhiteSpace(result.Token))
            {
                await DisplayAlert("Eroare", "Login esuat.", "OK");
                return;
            }

            Preferences.Set("auth_token", result.Token);
            Preferences.Set("user_email", result.Email);

            await DisplayAlert("Succes", "Te-ai logat.", "OK");

            await Navigation.PopAsync();
        }
    }
}
