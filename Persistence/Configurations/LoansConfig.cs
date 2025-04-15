using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LoansConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.LoanId);

            builder.Property(l => l.LoanDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(l => l.isReturned)
                .IsRequired();

            builder.Property(l => l.ReturnDate)
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);

            builder.HasOne(u => u.User)
                   .WithMany(l => l.Loans)
                   .HasForeignKey(u => u.UserId);
        }
    }
}
