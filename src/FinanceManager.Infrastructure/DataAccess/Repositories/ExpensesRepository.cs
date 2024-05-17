using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(FinanceManagerDbContext dbContext) : IExpenseReadOnlyRepository, IExpenseWriteOnlyRepository
{
    private readonly FinanceManagerDbContext _dbContext = dbContext;

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    public async Task<Expense?> GetById(long id) => await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
}



