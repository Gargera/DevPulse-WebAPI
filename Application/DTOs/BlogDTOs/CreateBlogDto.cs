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

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public string UserId { get; set; } = null!;
    }
}