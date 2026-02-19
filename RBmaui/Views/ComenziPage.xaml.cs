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
    }
}
