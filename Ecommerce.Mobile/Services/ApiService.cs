
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Responses;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace Ecommerce.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, ssl) => true
            };
            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
            _baseUrl = ConfigService.GetConfigService();
            Debug.WriteLine($"URL Configurada: {_baseUrl}");
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Debug.WriteLine($"Realizando solicitud de GET:  {_baseUrl}");
                var response = await _httpClient.GetAsync(_baseUrl + "countries");
                Debug.WriteLine($"Codigo de respuesta: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Codigo de respuesta: {response.StatusCode}");

                    await Application.Current.MainPage.DisplayAlert($"Error de API", $"Error al obtener los paises. Codigo: {response.StatusCode}", "OK");
                    return new List<Country>();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Respuesta obtenida {content}");

                    try
                    {
                        var result = JsonSerializer.Deserialize<List<Country>>(content, _serializerOptions);
                        return result ?? new List<Country>();
                    }
                    catch (JsonException e)
                    {
                        Debug.WriteLine($"Error al deserializar {e.Message}");
                        return new List<Country>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Error al intentar obtener los paises {ex.Message}");
                string ErrorMessage = "Error de conexion a la api";
                if (ex.Message.Contains("certificate") || ex.Message.Contains("SSL"))
                {
                    ErrorMessage = "Error de certificado SSL. Verifica la configuración de desarrollo";
                }
                else if (ex.Message.Contains("connection"))
                {
                    ErrorMessage = "No se pudo conectar al servidor. Verificar que la API esta en ejecucion";
                }

                await Application.Current.MainPage.DisplayAlert("Error de conexion", ErrorMessage, "OK");
                return new List<Country>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error general al obtener paises: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Error inesperado", $"{ex.Message}");
                return new List<Country>();
            }
        }

        public async Task<Country> GetCountryAsync(int id)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.GetAsync($"{_baseUrl + "countries"}/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Country>(content, _serializerOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener el pais {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize<Country>(country, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl + "countries"}/{country.Id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar el pais {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddCountryAsync(Country country)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var json = JsonSerializer.Serialize<Country>(country, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "countries", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al añadir el pais {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.DeleteAsync($"{_baseUrl + "countries"}/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al borrar el pais {ex.Message}");
                return false;
            }
        }


        //Categorias
        public async Task<List<Category>> GetCategoriesAsync()
        {

            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                Debug.WriteLine($"Realizando solicitud de GET:  {_baseUrl}");
                var response = await _httpClient.GetAsync(_baseUrl + "categories");
                Debug.WriteLine($"Codigo de respuesta: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Codigo de respuesta: {response.StatusCode}");

                    await Application.Current.MainPage.DisplayAlert($"Error de API", $"Error al obtener las categorias. Codigo: {response.StatusCode}", "OK");
                    return new List<Category>();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Respuesta obtenida {content}");

                    try
                    {
                        var result = JsonSerializer.Deserialize<List<Category>>(content, _serializerOptions);
                        return result ?? new List<Category>();
                    }
                    catch (JsonException e)
                    {
                        Debug.WriteLine($"Error al deserializar {e.Message}");
                        return new List<Category>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Error al intentar obtener las categorias {ex.Message}");
                string ErrorMessage = "Error de conexion a la api";
                if (ex.Message.Contains("certificate") || ex.Message.Contains("SSL"))
                {
                    ErrorMessage = "Error de certificado SSL. Verifica la configuración de desarrollo";
                }
                else if (ex.Message.Contains("connection"))
                {
                    ErrorMessage = "No se pudo conectar al servidor. Verificar que la API esta en ejecucion";
                }

                await Application.Current.MainPage.DisplayAlert("Error de conexion", ErrorMessage, "OK");
                return new List<Category>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error general al obtener categorias: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Error inesperado", $"{ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<Country> GetCategoryAsync(int id)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.GetAsync($"{_baseUrl + "categories"}/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Country>(content, _serializerOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener el pais {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var json = JsonSerializer.Serialize<Category>(category, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl + "categories"}/{category.Id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar la categoria {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO category)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var json = JsonSerializer.Serialize<CategoryDTO>(category, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "categories", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error en API: {response.StatusCode} - {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al añadir la categoria {ex.Message}");
                return false;
            }
        }


        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.DeleteAsync($"{_baseUrl + "categories"}/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al borrar la categoria {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Login(LoginDTO mode)
        {
            try
            {
                var json = JsonSerializer.Serialize(mode, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}accounts/Login", content);

                var jsonS = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta API: {jsonS}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Error en API: {response.StatusCode}");
                    return false;
                }

                TokenResponse? token;

                try
                {
                    token = JsonSerializer.Deserialize<TokenResponse>(jsonS, _serializerOptions);
                    Debug.WriteLine($"Token deserializado: {token?.Token}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al deserializar el token: {ex.Message}");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(token?.Token))
                {
                    Debug.WriteLine("Token nulo o vacío.");
                    return false;
                }

                await SecureStorage.SetAsync("auth_token", token.Token);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al iniciar sesión: {ex.Message}");
                return false;
            }
        }

        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("auth_token") ?? string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error al obtener token: {ex.Message}");
                return string.Empty;
            }
        }


    }

}


