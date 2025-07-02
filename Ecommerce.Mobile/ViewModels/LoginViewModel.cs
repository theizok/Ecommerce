using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Mobile.Services;
using Ecommerce.Shared.DTOs;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ecommerce.Mobile.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private string emailEntry;

    [ObservableProperty]
    private string passwordEntry;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private bool isErrorVisible;

    public ICommand LoginClickedCommand { get;}

    public LoginViewModel(ApiService apiService)
    {
        _apiService = apiService;
        LoginClickedCommand = new Command(async () => await OnLoginClicked());
    }


    public async Task OnLoginClicked()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine(">>> Ejecutando OnLoginClicked");
            IsErrorVisible = false;

            if (string.IsNullOrWhiteSpace(EmailEntry) || string.IsNullOrWhiteSpace(PasswordEntry))
            {
                ErrorMessage = "Todos los campos son obligatorios.";
                IsErrorVisible = true;
                return;
            }

            var loginDTO = new LoginDTO { Email = EmailEntry, Password = PasswordEntry };
            bool success = await _apiService.Login(loginDTO);

            if (success)
            {
                System.Diagnostics.Debug.WriteLine(">>> Login exitoso");

                await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");

                System.Diagnostics.Debug.WriteLine(">>> Cambiando AppShell");

                //Redirección
                var app = Application.Current as App;
                if (app != null)
                {
                    System.Diagnostics.Debug.WriteLine(">>> Redirigiendo a AppShell");
                    app.ShowAppShell();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ 'App' es null");
                }

            }
            else
            {
                ErrorMessage = "Credenciales incorrectas.";
                IsErrorVisible = true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"❌ Excepción en OnLoginClicked: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Fallo", "Inicio de sesión fallo", "Fallo");

        }
    }

}
