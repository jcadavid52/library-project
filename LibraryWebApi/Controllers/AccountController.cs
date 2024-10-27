using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.AddUser(addUserDto);

            return Ok(new {message = "Registrado exitosamente",user=addUserDto });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var resultLogin = await _userService.Login(loginDto);

            if(resultLogin == null)
            {
                return NotFound(new {message = "Usuario o clave inválida"});
            }

            return Ok(resultLogin);
        }


    }
}
