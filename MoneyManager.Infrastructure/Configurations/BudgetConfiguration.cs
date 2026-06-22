using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Configurations;

public class BudgetConfiguration
    : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.AmountLimit)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(b => b.StartDate)
               .IsRequired();

        builder.Property(b => b.EndDate)
               .IsRequired();

        builder.Property(b => b.CreatedAt)
               .IsRequired();

        builder.Property(b => b.UpdatedAt)
               .IsRequired();

        builder.HasOne(b => b.User)
               .WithMany(u => u.Budgets)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Category)
               .WithMany(c => c.Budgets)
               .HasForeignKey(b => b.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}