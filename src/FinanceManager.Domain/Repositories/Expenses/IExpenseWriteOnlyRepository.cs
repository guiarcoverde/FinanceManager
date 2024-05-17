using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Expenses;
public interface IExpenseWriteOnlyRepository
{
    Task Add(Expense expense);
}
