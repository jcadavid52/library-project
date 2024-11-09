using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime DateCreation { get; set; }
        public string CodeReference { get; set; }
        public AuthorDto Author { get; set; }
        public GeneroDto Genero { get; set; }


    }
}
