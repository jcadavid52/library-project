using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string User { get; set; }

        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string Password { get; set; }
    }
}
