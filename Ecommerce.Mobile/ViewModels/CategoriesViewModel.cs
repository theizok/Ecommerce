using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Mobile.Services;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Mobile.ViewModels
{
    public partial class CategoriesViewModel : ObservableValidator
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category _selectedCategory;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _newCategoryName;

        [ObservableProperty]
        private string _errorMessage;

        public CategoriesViewModel(ApiService apiService) 
        {
            _apiService = apiService;
            categories = new ObservableCollection<Category>();
            _ = Task.Run(async () => await LoadCategories());
        }


        [RelayCommand]
        public async Task LoadCategories()
        {


            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var categories = await _apiService.GetCategoriesAsync();

                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se puedo cargar categorias {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
            }
            finally { IsBusy = false; }

        }

        [RelayCommand]
        public async Task AddCategory()
        {
            if (string.IsNullOrWhiteSpace(NewCategoryName))
            {
                ErrorMessage = "El nombre de la categoria no puede estar vacio";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
            }
            else if (NewCategoryName.Length > 100)
            {
                ErrorMessage = "El nombre de la categoria no puede tener mas de 100 caracteres";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
            }
            else if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var newCategory = new CategoryDTO
                {
                    Name = NewCategoryName,
                };

                bool success = await _apiService.AddCategoryAsync(newCategory);

                if (success)
                {
                    await LoadCategories();
                    NewCategoryName = string.Empty;
                }
                else
                {
                    ErrorMessage = "Error no se pudo agregar la categoria";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error no se pudo agregar la categoria {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
            }
            finally { IsBusy = false; }
        }


        [RelayCommand]
        public async Task EditCategory()
        {
            if (SelectedCategory == null)
                return;

            // Aquí se implementaría la navegación a la página de edición
            // Por ahora, mostraremos un cuadro de diálogo sencillo para editar
            string result = await Shell.Current.DisplayPromptAsync("Editar Categoria",
                "Ingrese el nuevo nombre:", initialValue: SelectedCategory.Name);

            if (!string.IsNullOrWhiteSpace(result))
            {
                if (result.Length > 100)
                {
                    ErrorMessage = "El nombre no puede tener más de 100 caracteres";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
                    return;
                }

                var categoryToUpdate = new Category
                {
                    Id = SelectedCategory.Id,
                    Name = result,
                    ProductCategories = SelectedCategory.ProductCategories
                };

                bool success = await _apiService.UpdateCategoryAsync(categoryToUpdate);

                if (success)
                {
                    await LoadCategories();
                }
                else
                {
                    ErrorMessage = "No se pudo actualizar la categoria";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
                }
            }
        }

        [RelayCommand]
        public async Task DeleteCategory()
        {
            if (SelectedCategory == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Confirmar", $"Esta seguro que desea eliminar {SelectedCategory.Name}?", "Si", "No");

            if (confirm)
            {
                try
                {
                    IsBusy = true;
                    ErrorMessage = "";
                    bool success = await _apiService.DeleteCategoryAsync(SelectedCategory.Id);
                    if (success)
                    {
                        Categories.Remove(SelectedCategory);
                        SelectedCategory = null;
                    }
                    else
                    {
                        ErrorMessage = "No se puede eliminar la categoria seleccionada";
                        await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error al eliminar la categoria {ex.Message}";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
                }
                finally { IsBusy = false; }
            }
        }

        [RelayCommand]
        public void CategorySelected(Category category)
        {
            if (category != null)
                SelectedCategory = category;
        }

    }
}
