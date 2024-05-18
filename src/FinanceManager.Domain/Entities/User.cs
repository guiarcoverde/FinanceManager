namespace FinanceManager.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<Income> Incomes { get; set; } = [];
    public virtual ICollection<Expense> Expenses { get; set; } = [];

}