namespace MoneyManager.Application.DTOs.Expense;

public class UpdateExpenseDto
{
public decimal Amount { get; set; }

public string Description { get; set; } = string.Empty;

public DateTime ExpenseDate { get; set; }

public int CategoryId { get; set; }

}
