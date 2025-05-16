using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class LoginDTO
    {
        [Display(Name ="Documento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100)]
        public string Document { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100)]
        public string Password { get; set; }

    }
}
