using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Interfaces;

public interface IIncomeRepository
{
Task<IEnumerable<Income>> GetAllAsync();

Task<Income?> GetByIdAsync(int id);

Task AddAsync(Income income);

Task UpdateAsync(Income income);

Task DeleteAsync(Income income);

}
