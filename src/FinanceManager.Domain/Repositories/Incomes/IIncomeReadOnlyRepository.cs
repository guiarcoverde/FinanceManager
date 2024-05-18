using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Incomes;

public interface IIncomeReadOnlyRepository
{
    public Task<List<Income>> GetAll();
    public Task<Income?> GetById(long id);
}