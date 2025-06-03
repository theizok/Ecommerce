using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre")]
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("productCategoryId")]
        public int? ProductCategoryId { get; set; }

    }
}
