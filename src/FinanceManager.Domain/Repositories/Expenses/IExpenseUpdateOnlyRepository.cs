using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Expenses;

public interface IExpenseUpdateOnlyRepository
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);
    
}