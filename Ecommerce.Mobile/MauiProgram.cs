using CommunityToolkit.Maui;
using Ecommerce.Mobile.Services;
using Ecommerce.Mobile.ViewModels;
using Ecommerce.Mobile.Views;

using Microsoft.Extensions.Logging;

namespace Ecommerce.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servicios
            builder.Services.AddSingleton<ApiService>();

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<CountriesViewModel>();
            builder.Services.AddSingleton<CategoriesViewModel>();

            // Páginas
            builder.Services.AddTransient<LoginPage>(); // fuera del Shell
            builder.Services.AddSingleton<MainPage>();  // dentro del Shell
            builder.Services.AddSingleton<CategoriesPage>();

            // Shell
            builder.Services.AddSingleton<AppShell>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
