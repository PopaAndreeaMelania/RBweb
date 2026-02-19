using RBmaui.ViewModels;

namespace RBmaui.Views
{
    public partial class ComenziPage : ContentPage
    {
        private readonly ComenziViewModel vm = new();

        public ComenziPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.Incarca();
        }
        private async void OnRecenzieClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int comandaId = (int)button.CommandParameter;

            await Navigation.PushAsync(new AddRecenziePage(comandaId));
        }
    }
}
