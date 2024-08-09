using FinanceManager.Domain.Entities;

namespace WebApi.Test.Resources;

public class ExpenseIdentityManager
{
    private readonly Expense _expenses;

    public ExpenseIdentityManager(Expense expenses)
    {
        _expenses = expenses;
    }

    public long GetId() => _expenses.Id;
}