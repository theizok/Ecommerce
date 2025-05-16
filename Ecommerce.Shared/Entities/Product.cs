using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name="Producto")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        
        public ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductImage ProductImage { get; set; }
        
        public int ProductImageId { get; set; }
    }
}
