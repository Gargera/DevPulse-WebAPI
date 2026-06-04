using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.BlogDTOs
{
    public class CreateBlogDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(10000, MinimumLength = 20)]
        public string Content { get; set; } = null!;

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CategoryName { get; set; } = string.Empty!;
    }
}