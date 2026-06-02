using Domain.Entities;

namespace Application.DTOs.BlogDTOs
{
    public class GetBlogDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? ImagePath { get; set; }

        public string CategoryName { get; set; } = null!;

        public string UserName { get; set; } = null!;
    }
}