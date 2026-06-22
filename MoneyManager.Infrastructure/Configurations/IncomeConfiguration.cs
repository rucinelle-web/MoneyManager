using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Configurations;

public class IncomeConfiguration
    : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Amount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(i => i.Description)
               .HasMaxLength(500);

        builder.Property(i => i.IncomeDate)
               .IsRequired();

        builder.Property(i => i.CreatedAt)
               .IsRequired();

        builder.Property(i => i.UpdatedAt)
               .IsRequired();

        builder.HasOne(i => i.User)
               .WithMany(u => u.Incomes)
               .HasForeignKey(i => i.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Category)
               .WithMany(c => c.Incomes)
               .HasForeignKey(i => i.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}