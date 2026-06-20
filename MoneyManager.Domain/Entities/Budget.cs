namespace MoneyManager.Domain.Entities;

public class Budget
{
    public int Id { get; set; }

    public decimal AmountLimit { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}