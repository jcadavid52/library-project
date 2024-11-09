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
            return await _DBContext.Reservations
                .Include(r => r.User)
                  .ThenInclude(r => r.EducativeInstitution)
                .Include(r => r.books)
                  .ThenInclude(b => b.Category)
                .Include(r => r.books)
                  .ThenInclude(b => b.Author)
                .ToListAsync();

        }

        public async Task<IEnumerable<Reservation>> GetReservationsByIdUser(string idUser)
        {
            return await _DBContext.Reservations
                .Where(r => r.IdUser == idUser)
                 .Include(r => r.User)
                  .ThenInclude(r => r.EducativeInstitution)
                .Include(r => r.books)
                  .ThenInclude(b => b.Category)
                .Include(r => r.books)
                  .ThenInclude(b => b.Author)
                .ToListAsync();

        }

        public async Task RegisterReservation(Reservation reservation, IEnumerable<int> idBooks)
        {
            await _DBContext.Reservations.AddAsync(reservation);
            await SaveChanges();

            foreach (var idBook in idBooks)
            {

                await _DBContext.Database.ExecuteSqlRawAsync(
                "EXEC RegisterReservationBook_sp @IdReservation,@IdBook",
                    new SqlParameter("@IdReservation", reservation.Id),
                    new SqlParameter("@IdBook", idBook)

                );
            }



        }

        public async Task SaveChanges()
        {
            await _DBContext.SaveChangesAsync();
        }
    }
}
