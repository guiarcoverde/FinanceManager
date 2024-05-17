using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Expenses;
public interface IExpenseWriteOnlyRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function returns TRUE if the deletion was successful
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
