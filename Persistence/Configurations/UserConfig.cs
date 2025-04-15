using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(u => u.LastName)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(u => u.Address)
                   .HasMaxLength(200);

            builder.Property(p => p.Age)
                   .HasComputedColumnSql(
                                    "DATEDIFF(YEAR, BirthDate, GETDATE()) - " +
                                    "CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, BirthDate, GETDATE()), BirthDate) > GETDATE() " +
                                    "THEN 1 ELSE 0 END"
                    );


            builder.Property(p => p.BirthDate);

            builder.Property(p => p.ContactNumber)
                .HasMaxLength(12); // 000-000-0000

            builder.Property(p => p.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(p => p.LastModifiedBy)
                   .HasMaxLength(100);

            builder.HasMany(l => l.Loans)
                   .WithOne(u => u.User)
                   .HasForeignKey(u => u.UserId);
        }
    }
}
