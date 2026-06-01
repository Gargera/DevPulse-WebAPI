using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.ToTable(t => { 
                t.HasCheckConstraint("CK_ApplicationUser_FirstName_Length", "LEN(FirstName) BETWEEN 2 AND 100");
                t.HasCheckConstraint("CK_ApplicationUser_LastName_Length", "LEN(LastName) BETWEEN 2 AND 100");
            });
        }
    }
}