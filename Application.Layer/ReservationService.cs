using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Application.Layer
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookRepository _bookRepository;
        public ReservationService(IReservationRepository reservationRepository,
            IHttpContextAccessor httpContextAccessor,
            IBookRepository bookRepository)
        {
            _reservationRepository = reservationRepository;
            _httpContextAccessor = httpContextAccessor;
            _bookRepository = bookRepository;
        }
        public async Task AddReservation(AddReservationDto addReservationDto)
        {
            foreach(var idBook in addReservationDto.IdBooks)
            {
                var bookResult = await _bookRepository.GetBook(idBook);

                if(bookResult == null)
                {
                    throw new InvalidOperationException("Algún libro que intenta reservar, no existe");
                }

                if(bookResult.CountAvailable == 0)
                {
                    throw new InvalidOperationException($"El libro '{bookResult.Title}' no está disponible en el momento.");
                }
            }



            var user = _httpContextAccessor.HttpContext.User;

           
            var idUser = user.FindFirst(ClaimTypes.UserData)?.Value;

            var reservationsFilter = await _reservationRepository.GetReservationsByIdUser(idUser);

            foreach(var reservationItem in reservationsFilter)
            {
                if(reservationItem.DateFinish.Date >= DateTime.Now.Date)
                {
                    throw new InvalidOperationException("Ya tienes una reserva activa, modifica tu reserva");
                }
            }

            var reservation = new Reservation
            {
                DateInitial = addReservationDto.DateInitial,
                DateFinish = addReservationDto.DateFinish,
                DateCreate = DateTime.Now,
                IdUser = idUser
            };
            
            await _reservationRepository.RegisterReservation(reservation,addReservationDto.IdBooks);
        }

        public Task<IEnumerable<ReservationDto>> GetAllReservations()
        {
            throw new NotImplementedException();
        }
    }
}
