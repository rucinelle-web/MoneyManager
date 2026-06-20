using Microsoft.EntityFrameworkCore;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Income> Incomes { get; set; }

    public DbSet<Budget> Budgets { get; set; }

    public DbSet<Notification> Notifications { get; set; }
}