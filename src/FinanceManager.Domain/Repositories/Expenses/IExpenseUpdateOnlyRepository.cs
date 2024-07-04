using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Expenses;

public interface IExpenseUpdateOnlyRepository
{
    Task<Expense?> GetById(User user,long id);
    void Update(Expense expense);
    
}