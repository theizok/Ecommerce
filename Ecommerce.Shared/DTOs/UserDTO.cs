using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Entities;

namespace Ecommerce.Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo de contraseña debe de tener {2} minimo y maximo {1} caracteres")]

        public string Password { get; set; } = null!;


        [Compare("Password",ErrorMessage = "La contraseña de confirmacion debe de ser igual")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmación contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo de contraseña debe de tener {2} minimo y maximo {1} caracteres")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
