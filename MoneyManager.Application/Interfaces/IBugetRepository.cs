using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Interfaces;

public interface IBudgetRepository
{
Task<IEnumerable<Budget>> GetAllAsync();

Task<Budget?> GetByIdAsync(int id);

Task AddAsync(Budget budget);

Task UpdateAsync(Budget budget);

Task DeleteAsync(Budget budget);

}
