using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Expenses;
public interface IExpenseRepository
{
    Task Add(Expense expense);
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
