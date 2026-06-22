using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Configurations;

public class ExpenseConfiguration
    : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(e => e.Description)
               .HasMaxLength(500);

        builder.Property(e => e.ExpenseDate)
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .IsRequired();

        builder.Property(e => e.UpdatedAt)
               .IsRequired();

        builder.HasOne(e => e.User)
               .WithMany(u => u.Expenses)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Category)
               .WithMany(c => c.Expenses)
               .HasForeignKey(e => e.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}