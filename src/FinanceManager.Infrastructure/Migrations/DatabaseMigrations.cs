using FinanceManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Migrations;
public static class DatabaseMigrations
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FinanceManagerDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
