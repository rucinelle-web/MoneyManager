namespace MoneyManager.Application.DTOs.Budget;

public class UpdateBudgetDto
{
public decimal AmountLimit { get; set; }

public DateTime StartDate { get; set; }

public DateTime EndDate { get; set; }

public int CategoryId { get; set; }

}
