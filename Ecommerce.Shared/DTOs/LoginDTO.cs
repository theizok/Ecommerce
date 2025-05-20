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
        [Display(Name ="Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo valido")]
        [MaxLength(100)]
        public string Email { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(6, ErrorMessage = "El campo de contraseña debe de tener almenos {1}  caracteres")]
        public string Password { get; set; }

    }
}
