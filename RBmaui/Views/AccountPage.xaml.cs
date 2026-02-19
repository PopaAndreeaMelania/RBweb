using RBmaui.Helpers;

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

            if (!AuthSession.IsLoggedIn)
            {
                EmailLabel.Text = "Neautentificat";
                RoleLabel.Text = "";

                LoginBtn.IsVisible = true;
                RegisterBtn.IsVisible = true;

                MyOrdersBtn.IsVisible = false;
                LogoutBtn.IsVisible = false;
                return;
            }

            EmailLabel.Text = $"Logat ca: {AuthSession.Email}";
            RoleLabel.Text = string.IsNullOrWhiteSpace(AuthSession.Role) ? "" : $"Rol: {AuthSession.Role}";

            LoginBtn.IsVisible = false;
            RegisterBtn.IsVisible = false;

            MyOrdersBtn.IsVisible = true;
            LogoutBtn.IsVisible = true;
        }

        private async void Login_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new LoginPage());

        private async void Register_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new RegisterPage());

        private async void Comenzi_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new ComenziPage());

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            App.Database.ClearAuthHeader();

            Preferences.Remove("auth_token");
            Preferences.Remove("user_email");
            Preferences.Remove("user_role");

            await DisplayAlert("OK", "Te-ai delogat.", "OK");
            Application.Current.MainPage = new NavigationPage(new MeniuPage());
        }
    }
}
