using Ecommerce.Mobile.Views;

namespace Ecommerce.Mobile
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // Mostrar Login al inicio
            MainPage = new NavigationPage(_serviceProvider.GetRequiredService<LoginPage>());
        }

        public void ShowAppShell()
        {
            var shell = _serviceProvider.GetRequiredService<AppShell>();
            MainPage = shell;
        }
    }

}