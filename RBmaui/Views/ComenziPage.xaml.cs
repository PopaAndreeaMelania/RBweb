using RBmaui.ViewModels;

namespace RBmaui.Views
{
    public partial class ComenziPage : ContentPage
    {
        private readonly ComenziViewModel _vm = new();

        public ComenziPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string email = "test@client.com"; // temporar pana la login
            await _vm.Incarca(email);
        }
        private async void ComenzileMele_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComenziPage());
        }

    }
}
