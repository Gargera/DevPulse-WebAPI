using Domain.Entities;

namespace Application.DTOs.CategoryDTOs
{
    public class GetCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}