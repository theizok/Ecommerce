using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Entities;

namespace Ecommerce.Shared.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Display(Name="CategoriaProducto")]
        [Required(ErrorMessage ="El campo {o} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }

        [Display(Name = "Categorias")]

        public int CategoriesNumber => Categories == null ? 0 : Categories.Count; 

        public ICollection<Product> Products { get; set; }
        
        [Display(Name = "Productos")]
        public int ProductsNumber => Products == null ? 0 : Products.Count();

        [Display(Name = "Imagenes")]
        public ICollection<ProductImage> ProductImages { get; set; }


    } 
}
