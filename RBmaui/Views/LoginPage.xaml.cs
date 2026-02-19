using Microsoft.Maui.Storage;
using RBmaui.Helpers;

namespace RBmaui.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void GoRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
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
                var err = Preferences.Get("last_login_error", "Fara detalii");
                await DisplayAlert("Eroare", $"Login esuat.\n{err}", "OK");
                return;
            }


            Preferences.Set("auth_token", result.Token);
            Preferences.Set("user_email", result.Email);

            var firstRole = result.Roles != null && result.Roles.Count > 0 ? result.Roles[0] : "";
            Preferences.Set("user_role", firstRole);

            await DisplayAlert("Succes", "Te-ai logat.", "OK");
            await Navigation.PopAsync();
        }
    }
}
