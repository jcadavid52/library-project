using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Models
{
    [Keyless]
    public class ReservationBook
    {
        public int IdBook { get; set; }
        public int IdReservation { get; set; }

        [ForeignKey("IdBook")]
        public Book Book { get; set; }

        [ForeignKey("IdReservation")]
        public Reservation Reservation { get; set; }


    }
}
