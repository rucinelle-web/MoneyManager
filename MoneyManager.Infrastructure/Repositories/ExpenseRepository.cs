using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Infrastructure.Data;

namespace MoneyManager.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
private readonly ApplicationDbContext _context;

public ExpenseRepository(ApplicationDbContext context)
{
    _context = context;
}

public async Task<IEnumerable<Expense>> GetAllAsync()
{
    return await _context.Expenses
        .Include(e => e.Category)
        .ToListAsync();
}

public async Task<IEnumerable<Expense>> GetByUserIdAsync(string userId)
{
    return await _context.Expenses
        .Include(e => e.Category)
        .Where(e => e.UserId == userId)
        .ToListAsync();
}

public async Task<decimal> GetTotalByCategoryAsync(string userId, int categoryId)
{
    return await _context.Expenses
        .Where(e => e.UserId == userId &&
                    e.CategoryId == categoryId)
        .SumAsync(e => e.Amount);
}

public async Task<Expense?> GetByIdAsync(int id)
{
    return await _context.Expenses
        .Include(e => e.Category)
        .FirstOrDefaultAsync(e => e.Id == id);
}

public async Task AddAsync(Expense expense)
{
    await _context.Expenses.AddAsync(expense);
    await _context.SaveChangesAsync();
}

public async Task UpdateAsync(Expense expense)
{
    _context.Expenses.Update(expense);
    await _context.SaveChangesAsync();
}

public async Task DeleteAsync(Expense expense)
{
    _context.Expenses.Remove(expense);
    await _context.SaveChangesAsync();
}

}
