using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime DateCreation { get; set; }
        public string CodeReference { get; set; }
        public int IdAuthor { get; set; }
        public int IdCategory { get; set; }
        public int? ReservationId { get; set; }

        [ForeignKey("IdAuthor")]
        public Author Author { get; set; }

        [ForeignKey("IdCategory")]
        public Category Category { get; set; }

      
    }
}
