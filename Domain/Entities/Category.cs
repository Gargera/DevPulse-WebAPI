namespace Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
