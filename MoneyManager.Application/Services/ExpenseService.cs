using MoneyManager.Application.DTOs.Expense;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Services;

public class ExpenseService : IExpenseService
{
     private readonly IExpenseRepository _expenseRepository;
    private readonly IBudgetNotificationService _budgetNotificationService;

    public ExpenseService(
        IExpenseRepository expenseRepository,
        IBudgetNotificationService budgetNotificationService)
    {
        _expenseRepository = expenseRepository;
        _budgetNotificationService = budgetNotificationService;
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllAsync(string userId)
    {
        var expenses = await _expenseRepository.GetByUserIdAsync(userId);

        return expenses.Select(e => new ExpenseResponseDto
        {
            Id = e.Id,
            Amount = e.Amount,
            Description = e.Description,
            ExpenseDate = e.ExpenseDate,
            CategoryId = e.CategoryId,
            CategoryName = e.Category.Name
        });
    }

    public async Task<ExpenseResponseDto?> GetByIdAsync(int id, string userId)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);

        if (expense == null || expense.UserId != userId)
            return null;

        return new ExpenseResponseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Description = expense.Description,
            ExpenseDate = expense.ExpenseDate,
            CategoryId = expense.CategoryId,
            CategoryName = expense.Category.Name
        };
    }

    public async Task<ExpenseResponseDto> CreateAsync(
    CreateExpenseDto dto,
    string userId)
{
    var expense = new Expense
    {
        Amount = dto.Amount,
        Description = dto.Description,
        ExpenseDate = dto.ExpenseDate,
        CategoryId = dto.CategoryId,
        UserId = userId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    await _expenseRepository.AddAsync(expense);

    await _budgetNotificationService.CheckBudgetAsync(
        userId,
        dto.CategoryId);

    return new ExpenseResponseDto
    {
        Id = expense.Id,
        Amount = expense.Amount,
        Description = expense.Description,
        ExpenseDate = expense.ExpenseDate,
        CategoryId = expense.CategoryId,
        CategoryName = expense.Category?.Name ?? string.Empty
    };
}

public async Task<bool> UpdateAsync(
    int id,
    UpdateExpenseDto dto,
    string userId)
{
    var expense = await _expenseRepository.GetByIdAsync(id);

    if (expense == null)
        return false;

    if (expense.UserId != userId)
        return false;

    expense.Amount = dto.Amount;
    expense.Description = dto.Description;
    expense.ExpenseDate = dto.ExpenseDate;
    expense.CategoryId = dto.CategoryId;
    expense.UpdatedAt = DateTime.UtcNow;

    await _expenseRepository.UpdateAsync(expense);

    await _budgetNotificationService.CheckBudgetAsync(
        userId,
        expense.CategoryId);

    return true;
}

public async Task<bool> DeleteAsync(
    int id,
    string userId)
{
    var expense = await _expenseRepository.GetByIdAsync(id);

    if (expense == null)
        return false;

    if (expense.UserId != userId)
        return false;

    var categoryId = expense.CategoryId;

    await _expenseRepository.DeleteAsync(expense);

    await _budgetNotificationService.CheckBudgetAsync(
        userId,
        categoryId);

    return true;
}
}