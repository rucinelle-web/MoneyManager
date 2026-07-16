using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Services;

public class BudgetNotificationService : IBudgetNotificationService
{
private readonly IExpenseRepository _expenseRepository;
private readonly IBudgetRepository _budgetRepository;
private readonly INotificationRepository _notificationRepository;

public BudgetNotificationService(
    IExpenseRepository expenseRepository,
    IBudgetRepository budgetRepository,
    INotificationRepository notificationRepository)
{
    _expenseRepository = expenseRepository;
    _budgetRepository = budgetRepository;
    _notificationRepository = notificationRepository;
}

public async Task CheckBudgetAsync(string userId, int categoryId)
{
    var budget = await _budgetRepository
        .GetByUserAndCategoryAsync(userId, categoryId);

    if (budget == null)
    {
        return;
    }

    var totalExpenses = await _expenseRepository
        .GetTotalByCategoryAsync(userId, categoryId);

    var percentage =
        (totalExpenses / budget.AmountLimit) * 100;

    if (percentage >= 100)
    {
        await _notificationRepository.AddAsync(
            new Notification
            {
                Title = "Budget dépassé",
                Message = $"Vous avez dépassé votre budget de {budget.AmountLimit:N0} FCFA.",
                Type = NotificationType.BudgetExceeded,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId
            });
    }
    else if (percentage >= 80)
    {
        await _notificationRepository.AddAsync(
            new Notification
            {
                Title = "Budget presque atteint",
                Message = $"Vous avez utilisé {percentage:F0}% de votre budget.",
                Type = NotificationType.BudgetWarning,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId
            });
    }
}

}
