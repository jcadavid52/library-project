using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("RegisterReservation")]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> RegisterReservation([FromBody] AddReservationDto addReservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _reservationService.AddReservation(addReservationDto);

            return Ok(new {Message = "Reserva registrada exitosamente",Reservation = addReservationDto});
        }

        [HttpGet("GetAllReservations")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservations();

            return Ok(new
            {
                Reservations = reservations
            });
        }


        [HttpGet("GetAllReservationsByIdUser")]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetAllReservationsByIdUser(string idUser)
        {
            if (idUser.IsNullOrEmpty())
            {
                return NotFound();
            }

            var reservations = await _reservationService.GetReservationsByIdUser(idUser);

            return Ok(new
            {
                Reservations = reservations
            });
        }
    }
}
