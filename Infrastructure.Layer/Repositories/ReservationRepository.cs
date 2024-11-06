using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDBContext _DBContext;

        public ReservationRepository(LibraryDBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public async Task<IEnumerable<Reservation>> GetAllReservation()
        {
            return await _DBContext.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByIdUser(string idUser)
        {
            return await _DBContext.Reservations.Where(r => r.IdUser == idUser).ToListAsync();
        }

        public async Task RegisterReservation(Reservation reservation,int[] idBooks)
        {
            await _DBContext.Reservations.AddAsync(reservation);
            await SaveChanges();

            foreach(var Idbook in idBooks)
            {

                await _DBContext.Database.ExecuteSqlRawAsync(
                "EXEC RegisterReservationBook_sp @IdReservation,@IdBook",
                    new SqlParameter("@IdReservation", reservation.Id),
                    new SqlParameter("@IdBook", Idbook)

                );
            }



        }

        public async Task SaveChanges()
        {
            await _DBContext.SaveChangesAsync();
        }
    }
}
