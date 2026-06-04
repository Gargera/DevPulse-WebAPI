using Application.DTOs.BlogDTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevPulseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var response = await _blogService.GetAllBlogsAsync();

            return Ok(response.Data);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var response = await _blogService.GetBlogByIdAsync(id);
            
            if(!response.IsSuccess) return NotFound(response.Message);
            
            return Ok(response.Data);
        }

        [AllowAnonymous]
        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetBlogsByCategoryId(int categoryId)
        {
            var response = await _blogService.GetBlogsByCategoryIdAsync(categoryId);

            if (!response.IsSuccess) return NotFound(response.Message);

            return Ok(response.Data);
        }

        [Authorize]
        [HttpGet("my-blogs")]
        public async Task<IActionResult> GetMyBlogs()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null) return Unauthorized();

            var blogs = await _blogService.GetBlogsByUserIdAsync(userId);

            return Ok(blogs);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto blog)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _blogService.CreateBlogAsync(blog, userId);

            if(!response.IsSuccess) return BadRequest(response.Message);
            
            return Created();
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, UpdateBlogDto blog)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _blogService.UpdateBlogAsync(id, blog, userId);
            
            if(!response.IsSuccess) return BadRequest(response.Message);
            
            return NoContent();
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _blogService.DeleteBlogAsync(id, userId);
            
            if (!response.IsSuccess) return NotFound(response.Message);
          
            return NoContent();
        }
    }
}