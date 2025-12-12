using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using servweb1_t2.Domain.Entities;

namespace servweb1_t2.Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.StudentName)
                .IsRequired();
            builder.Property(l => l.LoanDate)
                .IsRequired();
            builder.Property(l => l.ReturnDate)
                .IsRequired(false);
            builder.Property(l => l.Status)
                .IsRequired();
            builder.Property(l => l.CreatedAt)
                .IsRequired();

            builder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}