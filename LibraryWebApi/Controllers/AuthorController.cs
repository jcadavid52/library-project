﻿using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAllAuthors")]
        //[Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();

            return Ok(new { Authors = authors });
        }

        [HttpPost("AddAuthor")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorDto addAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorService.AddAuthor(addAuthorDto);

            return Ok(new { Message = "Author agregado con éxito", Author = addAuthorDto });
        }

    }
}
