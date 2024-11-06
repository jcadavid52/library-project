using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime DateInitial { get; set; }
        public DateTime DateFinish { get; set; }
        public DateTime DateCreate { get; set; }
        public BookDto Book { get; set; }
        public  UserDto User { get; set; }
    }
}
