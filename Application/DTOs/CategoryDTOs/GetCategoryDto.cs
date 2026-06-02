using Application.DTOs.BlogDTOs;

namespace Application.DTOs.CategoryDTOs
{
    public class GetCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<GetBlogDto> Blogs { get; set; } = new List<GetBlogDto>();
    }
}