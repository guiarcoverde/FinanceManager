using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Incomes;

public interface IIncomeUpdateOnlyRepository
{
    Task<Income?> GetById(long id);
    void Update(Income expense);
}