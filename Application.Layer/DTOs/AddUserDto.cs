using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class AddUserDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Compare("Password",ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdEducativeInstitution { get; set; }
    }
}
