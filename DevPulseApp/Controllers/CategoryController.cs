using Application.DTOs.BlogDTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.CategoryDTOs;

namespace DevPulseApp.Controllers
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();

            return Ok(response.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);

            if (!response.IsSuccess) return NotFound(response.Message);

            return Ok(response.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            var response = await _categoryService.CreateCategoryAsync(category);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto category)
        {
            var response = await _categoryService.UpdateCategoryAsync(id, category);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            if (!response.IsSuccess) return NotFound(response.Message);

            return NoContent();
        }
    }
}