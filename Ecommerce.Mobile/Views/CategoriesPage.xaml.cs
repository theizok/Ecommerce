using Ecommerce.Mobile.ViewModels;

namespace Ecommerce.Mobile.Views;

public partial class CategoriesPage : ContentPage
{
	private readonly CategoriesViewModel _viewModel;
    public CategoriesPage(CategoriesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadCategories();

    }
    private void OnOpenMenuClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Esto abre el menú lateral
    }
}