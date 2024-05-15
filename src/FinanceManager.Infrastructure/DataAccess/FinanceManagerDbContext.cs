using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess;

public class FinanceManagerDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=financemanagerdb;Uid=root;Pwd=guigaa10";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
