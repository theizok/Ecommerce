﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entitites
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public State State { get; set; }

        public int StateId { get; set; }
    }
}
