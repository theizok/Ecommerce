using Ecommerce.Mobile.Views;

namespace Ecommerce.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("mainpage", typeof(MainPage));
            Routing.RegisterRoute("categories", typeof(CategoriesPage));
        }
    }
}
