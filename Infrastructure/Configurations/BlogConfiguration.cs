using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.Content)
                .IsRequired();

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.UserId);

            builder.ToTable(t => {
                t.HasCheckConstraint("CK_Blog_Title_Length", "LEN(Title) BETWEEN 3 AND 200");
                t.HasCheckConstraint("CK_Blog_Content_Length", "LEN(Content) BETWEEN 20 AND 10000");
            });
        }
    }
}