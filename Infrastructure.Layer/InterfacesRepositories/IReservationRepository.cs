using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.InterfacesRepositories
{
    public interface IReservationRepository
    {
        Task RegisterReservation(Reservation reservation,IEnumerable<int> idBooks);
        Task<IEnumerable<Reservation>> GetAllReservation();
        Task<IEnumerable<Reservation>> GetReservationsByIdUser(string idUser);
        Task SaveChanges();


    }
}
