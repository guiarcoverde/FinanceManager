using FinanceManager.Domain.Repositories;

namespace FinanceManager.Infrastructure.DataAccess;

internal class UnitOfWork : IUnityOfWork
{

    private readonly FinanceManagerDbContext _dbContext;

    public UnitOfWork(FinanceManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
