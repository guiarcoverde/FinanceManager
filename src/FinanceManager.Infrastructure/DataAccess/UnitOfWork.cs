using FinanceManager.Domain.Repositories;

namespace FinanceManager.Infrastructure.DataAccess;

internal class UnitOfWork(FinanceManagerDbContext dbContext) : IUnitOfWork
{
    public async Task Commit() => await dbContext.SaveChangesAsync();
}
