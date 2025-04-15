using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BooksConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .HasAnnotation("MinLength", 6)
                .HasAnnotation("MaxLength", 100)
                .IsRequired();


            builder.Property(b => b.Author)
                .HasAnnotation("MinLength", 6)
                .HasAnnotation("MaxLength", 80)
                .IsRequired();

            builder.Property(b => b.Genre)
                .HasAnnotation("MinLength", 6)
                .HasAnnotation("MaxLength", 50)
                .IsRequired();

            builder.Property(b => b.Stock);

            builder.Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.Description)
                .HasAnnotation("MaxLength", 200);

            builder.Property(b => b.CreatedBy)
                   .HasMaxLength(80);

            builder.Property(b => b.LastModifiedBy)
                   .HasMaxLength(80);

            builder.HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(b => b.BookId);

        }
    }
}
