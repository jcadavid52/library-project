
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet("GetAllBooks")]

        [Authorize(Roles = "Usuario,Admin")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok("Libros");
        }

        
        [HttpPost("AddBook")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AddBook()
        {
            return Ok("Libros");
        }
    }
}
