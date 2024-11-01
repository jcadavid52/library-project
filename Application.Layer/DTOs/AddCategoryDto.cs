using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }
    }
}
