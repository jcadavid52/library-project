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

               
                if(bookResult.ReservationId != null)
                {
                    throw new InvalidOperationException($"El libro '{bookResult.Title}' no está disponible en el momento.");
                }
                
                if(addReservationDto.IdBooks.Count() > 3)
                {
                    throw new InvalidOperationException("La cantidad máxima a reservar es de 3 libros");
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

            var deliveryDate = CalculateDaysReservation();

            var reservation = new Reservation
            {
                DateInitial =DateTime.Now.AddDays(1),
                DateFinish = deliveryDate,
                DateCreate = DateTime.Now,
                IdUser = idUser,
                
            };
            
            await _reservationRepository.RegisterReservation(reservation,addReservationDto.IdBooks);
        }

        public DateTime CalculateDaysReservation()
        {
            var dateNow = DateTime.Now;
            int countDaysReservation = 7;
            int countDays = 0;

            while (countDays < countDaysReservation)
            {
                dateNow = dateNow.AddDays(1);

                if(dateNow.DayOfWeek != DayOfWeek.Saturday && dateNow.DayOfWeek != DayOfWeek.Sunday)
                {
                    countDays++;
                }
            }

            return dateNow;


        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var reservationDtos = new List<ReservationDto>();

            var reservations = await _reservationRepository.GetAllReservation();

            foreach (var reservation in reservations)
            {
                var reservationDto = new ReservationDto
                {
                    Id = reservation.Id,
                    DateCreate = reservation.DateCreate,
                    DateFinish = reservation.DateFinish,
                    DateInitial = reservation.DateInitial,
                    User = new UserDto
                    {
                        Id = reservation.User.Id,
                        Name = reservation.User.Name,
                        Adress = reservation.User.Adress,
                        Email = reservation.User.Email,
                        Institution = reservation.User.EducativeInstitution.Name,
                        LastName = reservation.User.LastName,
                    },
                    BookDtos = reservation.books.Select(book => new BookDto
                    {
                        Id=book.Id,
                        Title=book.Title,
                        PageNumber=book.PageNumber,
                        Description=book.Description,
                        DateCreation=book.DateCreation,
                        DatePublication=book.DatePublication,
                        CodeReference=book.CodeReference,
                        Author = new AuthorDto
                        {
                            Id = book.Author.Id,
                            Name = book.Author.Name,
                            Nationality = book.Author.Nationality,
                            Birthdate = book.Author.Birthdate,
                        },
                        Genero = new GeneroDto
                        {
                            Id = book.Category.Id,
                            Name=book.Category.Name,
                        }
                    })
                };

                reservationDtos.Add(reservationDto);
            }

            return reservationDtos;
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByIdUser(string idUser)
        {
            var reservationDtos = new List<ReservationDto>();

            var reservations = await _reservationRepository.GetReservationsByIdUser(idUser);

            foreach (var reservation in reservations)
            {
                var reservationDto = new ReservationDto
                {
                    Id = reservation.Id,
                    DateCreate = reservation.DateCreate,
                    DateFinish = reservation.DateFinish,
                    DateInitial = reservation.DateInitial,
                    User = new UserDto
                    {
                        Id = reservation.User.Id,
                        Name = reservation.User.Name,
                        Adress = reservation.User.Adress,
                        Email = reservation.User.Email,
                        Institution = reservation.User.EducativeInstitution.Name,
                        LastName = reservation.User.LastName,
                    },
                    BookDtos = reservation.books.Select(book => new BookDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        PageNumber = book.PageNumber,
                        Description = book.Description,
                        DateCreation = book.DateCreation,
                        DatePublication = book.DatePublication,
                        CodeReference = book.CodeReference,
                        Author = new AuthorDto
                        {
                            Id = book.Author.Id,
                            Name = book.Author.Name,
                            Nationality = book.Author.Nationality,
                            Birthdate = book.Author.Birthdate,
                        },
                        Genero = new GeneroDto
                        {
                            Id = book.Category.Id,
                            Name = book.Category.Name,
                        }
                    })
                };

                reservationDtos.Add(reservationDto);
            }

            return reservationDtos;
        }
    }
}
