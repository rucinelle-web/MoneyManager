using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Infrastructure.Data;

namespace MoneyManager.Infrastructure.Repositories;

public class IncomeRepository : IIncomeRepository
{
private readonly ApplicationDbContext _context;

public IncomeRepository(ApplicationDbContext context)
{
    _context = context;
}

public async Task<IEnumerable<Income>> GetAllAsync()
{
    return await _context.Incomes
        .Include(i => i.Category)
        .ToListAsync();
}

public async Task<IEnumerable<Income>> GetByUserIdAsync(string userId)
{
    return await _context.Incomes
        .Where(i => i.UserId == userId)
        .Include(i => i.Category)
        .ToListAsync();
}

public async Task<Income?> GetByIdAsync(int id)
{
    return await _context.Incomes
        .Include(i => i.Category)
        .FirstOrDefaultAsync(i => i.Id == id);
}

public async Task AddAsync(Income income)
{
    await _context.Incomes.AddAsync(income);
    await _context.SaveChangesAsync();
}

public async Task UpdateAsync(Income income)
{
    _context.Incomes.Update(income);
    await _context.SaveChangesAsync();
}

public async Task DeleteAsync(Income income)
{
    _context.Incomes.Remove(income);
    await _context.SaveChangesAsync();
}


}
