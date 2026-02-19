using Microsoft.Maui.Storage;

namespace RBmaui.Views
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var token = Preferences.Get("auth_token", "");

            if (string.IsNullOrWhiteSpace(token))
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var email = Preferences.Get("user_email", "");
            var role = Preferences.Get("user_role", "");

            EmailLabel.Text = $"Logat ca: {email}";
            RoleLabel.Text = string.IsNullOrWhiteSpace(role) ? "" : $"Rol: {role}";
        }


        private async void Comenzi_Clicked(object sender, EventArgs e)
        {
            var email = Preferences.Get("user_email", "");
            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Info", "Trebuie sa te loghezi.", "OK");
                return;
            }

            await Navigation.PushAsync(new ComenziPage());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("auth_token");
            Preferences.Remove("user_email");
            Preferences.Remove("user_role");

            await DisplayAlert("OK", "Te-ai delogat.", "OK");
            
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            OnAppearing();
        }
        private async void Meniu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeniuPage());
        }


    }
}
