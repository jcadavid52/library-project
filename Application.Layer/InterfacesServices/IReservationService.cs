using Application.Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.InterfacesServices
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations();
        Task<IEnumerable<ReservationDto>> GetReservationsByIdUser(string idUser);
        Task AddReservation(AddReservationDto addReservationDto);
    }
}
