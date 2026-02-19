using RBmaui.Views;

namespace RBmaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // rute pentru navigare cu Shell.Current.GoToAsync(nameof(LoginPage))
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
