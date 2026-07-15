using MoneyManager.Application.DTOs.Expense;

namespace MoneyManager.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseResponseDto>> GetAllAsync(string userId);

    Task<ExpenseResponseDto?> GetByIdAsync(int id, string userId);

    Task<ExpenseResponseDto> CreateAsync(
        CreateExpenseDto dto,
        string userId);

    Task<bool> UpdateAsync(
        int id,
        UpdateExpenseDto dto,
        string userId);

    Task<bool> DeleteAsync(
        int id,
        string userId);
}