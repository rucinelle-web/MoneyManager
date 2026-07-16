using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Infrastructure.Data;

namespace MoneyManager.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
private readonly ApplicationDbContext _context;


public BudgetRepository(ApplicationDbContext context)
{
    _context = context;
}

public async Task<IEnumerable<Budget>> GetAllAsync()
{
    return await _context.Budgets
        .Include(b => b.Category)
        .ToListAsync();
}

public async Task<IEnumerable<Budget>> GetByUserIdAsync(string userId)
{
    return await _context.Budgets
        .Where(b => b.UserId == userId)
        .Include(b => b.Category)
        .ToListAsync();
}

public async Task<Budget?> GetByIdAsync(int id)
{
    return await _context.Budgets
        .Include(b => b.Category)
        .FirstOrDefaultAsync(b => b.Id == id);
}

public async Task AddAsync(Budget budget)
{
    await _context.Budgets.AddAsync(budget);
    await _context.SaveChangesAsync();
}

public async Task UpdateAsync(Budget budget)
{
    _context.Budgets.Update(budget);
    await _context.SaveChangesAsync();
}

public async Task DeleteAsync(Budget budget)
{
    _context.Budgets.Remove(budget);
    await _context.SaveChangesAsync();
}

public async Task<Budget?> GetByUserAndCategoryAsync(string userId, int categoryId)
{
    return await _context.Budgets
        .FirstOrDefaultAsync(b =>
            b.UserId == userId &&
            b.CategoryId == categoryId);
}


}
