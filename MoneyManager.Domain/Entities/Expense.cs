namespace MoneyManager.Domain.Entities;

public class Expense
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime ExpenseDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}