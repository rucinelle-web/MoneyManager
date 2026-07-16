using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Interfaces;

public interface IBudgetRepository
{
    Task<IEnumerable<Budget>> GetAllAsync();

    // Nouvelle méthode
    Task<IEnumerable<Budget>> GetByUserIdAsync(string userId);

    Task<Budget?> GetByIdAsync(int id);

    Task AddAsync(Budget budget);

    Task UpdateAsync(Budget budget);

    Task DeleteAsync(Budget budget);

    Task<Budget?> GetByUserAndCategoryAsync(string userId, int categoryId);
}