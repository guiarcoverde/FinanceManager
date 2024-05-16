using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess;

internal class FinanceManagerDbContext : DbContext
{
    public FinanceManagerDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
}
