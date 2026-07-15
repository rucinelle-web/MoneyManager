namespace MoneyManager.Application.Services;

public interface IBudgetNotificationService
{
Task CheckBudgetAsync(string userId, int categoryId);
}
