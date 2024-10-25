using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateInitial { get; set; }
        public DateTime DateFinish { get; set; }
        public  DateTime DateCreate { get; set; }
        public int IdBook { get; set; }
        public string IdUser { get; set; }

        [ForeignKey("IdBook")]
        public Book Book { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

    }
}
