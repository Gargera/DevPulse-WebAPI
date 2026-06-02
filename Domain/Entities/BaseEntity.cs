namespace Domain.Entities
{
    public class BaseEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; set; } = default!;
    }
}