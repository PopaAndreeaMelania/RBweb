using RBmaui.Data;
using RBmaui.Views;

namespace RBmaui
{
    public partial class App : Application
    {
        public static RBDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();

            Database = new RBDatabase(new RestService());

            MainPage = new NavigationPage(new MeniuPage());
        }
    }
}
