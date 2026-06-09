using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.BlogDTOs
{
    public class UpdateBlogDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(10000, MinimumLength = 20)]
        public string Content { get; set; } = null!;

        public IFormFile? Image { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CategoryName { get; set; }
    }
}