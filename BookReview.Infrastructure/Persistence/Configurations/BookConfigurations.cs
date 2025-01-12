using BookReview.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Infrastructure.Persistence.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder
               .Property(b => b.QuantityPages)
               .IsRequired();

            builder
               .Property(b => b.AuthorId)
               .IsRequired();

            builder
               .Property(b => b.Description)
               .IsRequired()
               .HasMaxLength(150);

            builder
               .Property(b => b.PublicationYear)
               .IsRequired();

            builder
               .Property(b => b.ISBN)
               .IsRequired();

            builder
               .Property(b => b.PublicationYear)
               .IsRequired();

            builder
               .HasOne(b => b.Author)
               .WithMany(a => a.Books)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
