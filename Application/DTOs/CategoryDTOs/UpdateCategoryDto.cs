using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;
    }
}