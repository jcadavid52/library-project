using System.ComponentModel.DataAnnotations;

namespace Application.Layer.DTOs
{
    public class AddReservationDto
    {
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //public DateTime DateInitial { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //public DateTime DateFinish { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public IEnumerable<int> IdBooks { get; set; }

    }
}
