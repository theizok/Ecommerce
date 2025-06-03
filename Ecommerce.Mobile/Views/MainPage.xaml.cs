using Ecommerce.Mobile.ViewModels;

namespace Ecommerce.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly CountriesViewModel _viewModel;

        public MainPage(CountriesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

               await  _viewModel.LoadCountries();
            
        }

        private void OnOpenMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true; // Esto abre el menú lateral
        }
    }
}