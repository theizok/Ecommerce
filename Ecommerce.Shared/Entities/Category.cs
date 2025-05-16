using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Entities;

namespace Ecommerce.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
