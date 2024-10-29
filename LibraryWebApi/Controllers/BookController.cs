
using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetAllBooks")]

        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }

        
        [HttpPost("AddBook")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookService.AddBook(book);

            return Ok(new { message = "Registrado exitosamente", book = book });
        }

        [HttpDelete("DeleteBook")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if(id == null || id < 1)
            {
                return NotFound();
            }
            await _bookService.DeleteBook(id);

            return Ok("Eliminado con exito");
        }

    }
}
