using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            
            return Ok(new { Categories = categories });
        }

        [HttpPost("AddCategory")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddCategory(addCategoryDto);

            return Ok(new { Message = "Categoría registrada con exito", Category = addCategoryDto });
        }
    }
}
