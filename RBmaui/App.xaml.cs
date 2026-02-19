using RBmaui.Data;
using RBmaui.Views;
using RBmaui.Helpers;


namespace RBmaui
{
    public partial class App : Application
    {
        public static RBDatabase Database { get; private set; }
        public static Cos Cos { get; private set; }

        public App()
        {
            InitializeComponent();

            Database = new RBDatabase(new RestService());
            Cos = new Cos();

            MainPage = new NavigationPage(new MeniuPage());
        }
    }
}
