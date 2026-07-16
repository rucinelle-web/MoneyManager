namespace MoneyManager.Application.DTOs.Income;

public class CreateIncomeDto
{
public decimal Amount { get; set; }


public string Description { get; set; } = string.Empty;

public DateTime IncomeDate { get; set; }

public int CategoryId { get; set; 
}
}
