namespace MoneyManager.Domain.Entities;

public class ApplicationUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<Category> Categories { get; set; }
    = new List<Category>();

    public ICollection<Expense> Expenses { get; set; }
    = new List<Expense>();

    public ICollection<Income> Incomes { get; set; }
    = new List<Income>();

    public ICollection<Budget> Budgets { get; set; }
    = new List<Budget>();

    public ICollection<Notification> Notifications { get; set; }
    = new List<Notification>();

}
