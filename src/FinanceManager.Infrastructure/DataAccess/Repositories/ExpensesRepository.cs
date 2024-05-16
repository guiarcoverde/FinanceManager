using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpenseRepository
{
    private readonly FinanceManagerDbContext _dbContext;
    public ExpensesRepository(FinanceManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }
}



