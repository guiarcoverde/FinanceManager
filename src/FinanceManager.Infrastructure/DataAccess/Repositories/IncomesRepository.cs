using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Incomes;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class IncomesRepository(FinanceManagerDbContext dbContext) : IIncomeWriteOnlyRepository, IIncomeReadOnlyRepository, IIncomeUpdateOnlyRepository
{
    private readonly FinanceManagerDbContext _dbContext = dbContext;
    
    /*
     * Register income use case
     */
    public async Task Add(Income income)
    {
        await _dbContext.Incomes.AddAsync(income);
    }

    /*
     * Get use cases
     */
    public async Task<List<Income>> GetAll() => await _dbContext.Incomes.AsNoTracking().ToListAsync();

    async Task<Income?> IIncomeReadOnlyRepository.GetById(long id) =>
        await _dbContext.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    
    /*
     * Delete use case
     */

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);

        if (result is null)
        {
            return false;
        }

        _dbContext.Incomes.Remove(result);
        return true;
    }
    
    /*
     * Update use case
     */
    async Task<Income?> IIncomeUpdateOnlyRepository.GetById(long id) =>
        await _dbContext.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

    public void Update(Income income)
    {
        _dbContext.Incomes.Update(income);
    }
    
}