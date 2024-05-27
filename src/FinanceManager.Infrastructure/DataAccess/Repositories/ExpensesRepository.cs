using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(FinanceManagerDbContext dbContext) : IExpenseReadOnlyRepository, IExpenseWriteOnlyRepository, IExpenseUpdateOnlyRepository
{
    private readonly FinanceManagerDbContext _dbContext = dbContext;

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);

        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);
        return true;
    }

    public async Task<List<Expense>> GetAll() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    async Task<Expense?> IExpenseReadOnlyRepository.GetById(long id) => await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(long id) => await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
    
    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);

        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);
        
        return await _dbContext.Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(expense => expense.Date)
            .ThenBy(expense => expense.Title)
            .ToListAsync();
        
        
    }

}



