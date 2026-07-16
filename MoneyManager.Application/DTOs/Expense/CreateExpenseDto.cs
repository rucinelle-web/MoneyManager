namespace MoneyManager.Application.DTOs.Expense;

public class CreateExpenseDto
{
public decimal Amount { get; set; }

public string Description { get; set; } = string.Empty;

public DateTime ExpenseDate { get; set; }

public int CategoryId { get; set; }

}
