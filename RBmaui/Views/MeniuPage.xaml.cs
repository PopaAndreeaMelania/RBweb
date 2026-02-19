using RBmaui.ViewModels;

namespace RBmaui.Views
{
    public partial class MeniuPage : ContentPage
    {
        private readonly MeniuViewModel _vm = new MeniuViewModel();

        public MeniuPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.IncarcaMeniu();
        }
    }
}
