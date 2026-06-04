namespace Domain.Entities
{
    public class Blog : BaseEntity<int>
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
    }
}