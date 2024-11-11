
using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            var books = await _bookService.GetAllBooks();

            return Ok(new {Books = books});
        }

        
        [HttpPost("AddBook")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AddBook([FromForm] AddBookDto book)
        {
            string pathGlobalImage = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookService.AddBook(book,pathGlobalImage);

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

        [HttpGet("GetBook")]

        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);

            if (id == null || id < 1 || book == null)
            {
                return NotFound();
            }

            

            return Ok(new { Book = book });
        }

        [HttpGet("GetBookByTitle")]

        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = await _bookService.GetBookByTitle(title);

            if (title.IsNullOrEmpty() || book == null)
            {
                return NotFound();
            }

            return Ok(new { Book = book });
        }

        [HttpPut("UpdateBook")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto bookDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == null || id < 1 || bookDto == null)
            {
                return NotFound();
            }

            await _bookService.UpdateBook(bookDto, id);

            return Ok(new {Message = "Modificado con éxito",BookUpdated = bookDto});
        }

    }
}
