using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Incomes;

public interface IIncomeReadOnlyRepository
{
    Task<List<Income>> GetAll();
    Task<Income?> GetById(long id);
}