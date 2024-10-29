using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class AddBookDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdAuthor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime DatePublication { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime DateCreation { get; set; }
    }
}
