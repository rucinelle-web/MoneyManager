using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Interfaces;

public interface IExpenseRepository
{
Task<IEnumerable<Expense>> GetAllAsync();

Task<IEnumerable<Expense>> GetByUserIdAsync(string userId);

Task<Expense?> GetByIdAsync(int id);

Task AddAsync(Expense expense);

Task UpdateAsync(Expense expense);

Task DeleteAsync(Expense expense);


}
