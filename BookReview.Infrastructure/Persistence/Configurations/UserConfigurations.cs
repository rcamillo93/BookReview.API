using BookReview.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u =>u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.Password)
                .IsRequired();

            builder
                .Property(u => u.Role)
                .HasMaxLength(100);
        }
    }
}
