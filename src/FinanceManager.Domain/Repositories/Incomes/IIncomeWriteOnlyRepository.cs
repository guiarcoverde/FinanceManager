using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Incomes;

public interface IIncomeWriteOnlyRepository
{
    Task Add(Income income);

    Task<bool> Delete(long id);
}